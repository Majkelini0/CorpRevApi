using EvilCorp.DTOs.PaymentDTOs;
using EvilCorp.Services.DealService;
using EvilCorp.Services.PaymentService;
using Microsoft.AspNetCore.Mvc;

namespace EvilCorp.Controllers;

[ApiController]
[Route("EvilCorp/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentService _paymentService;
    private readonly ISaleService _saleService;

    public PaymentsController(IPaymentService paymentService, ISaleService saleService)
    {
        _paymentService = paymentService;
        _saleService = saleService;
    }

    [HttpPost("NewPayment")]
    public async Task<IActionResult> MakeNewPaymentAsync([FromBody] NewPaymentDto request)
    {
        if (await _saleService.DoesSaleExistsAsync(request.SingleSaleId) == false)
        {
            return NotFound("Sale with this ID doesn't exist in the database");
        }

        if (await _saleService.IsSaleAlreadyPaidAsync(request.SingleSaleId))
        {
            return BadRequest("Sale with this ID is already paid");
        }


        if (await _saleService.IsSaleStillValidAsync(request.SingleSaleId))
        {
            var allPaymentsSum = await _paymentService.SumUpAllPaymentsAsync(request.SingleSaleId);

            if (await _paymentService.MakeNewPaymentTransactionAsync(request) == false)
            {
                return BadRequest("Payment failed");
            }

            if (await _paymentService.UpdateIsPaidParamAsync(request, allPaymentsSum) == false)
            {
                return BadRequest("Payment failed");
            }

            return Ok("Payment created");
        } // 'else'
        
        await _paymentService.RollBackPaymentsAsync(request.SingleSaleId);

        await _saleService.DeleteSaleAsync(request.SingleSaleId);


        return Ok("Payment was cancelled due to exceeding the deadline. All payments were rolled back. Sale was deleted");
    }
}