using EvilCorp.DTOs.DealDTOs;
using EvilCorp.DTOs.PaymentDTOs;
using EvilCorp.Models;
using EvilCorp.Services.ClientService;
using EvilCorp.Services.DealService;
using EvilCorp.Services.SoftwareService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvilCorp.Controllers;

[ApiController]
[Route("EvilCorp/[controller]")]
public class SalesController : ControllerBase
{
    private readonly ISaleService _saleService;
    private readonly IClientService _clientService;
    private readonly ISoftwareService _softwareService;
    
    public SalesController(ISaleService saleService, IClientService clientService, ISoftwareService softwareService)
    {
        _saleService = saleService;
        _clientService = clientService;
        _softwareService = softwareService;
    }

    [AllowAnonymous]
    [HttpPost("NewSale")]
    public async Task<IActionResult> MakeNewSaleAsync(NewSaleDto request)
    {
        if (await _clientService.DoesClientExistsAsync(request.ClientId) == false)
        {
            return NotFound("Client with this ID doesn't exist in the database");
        }

        if (await _softwareService.DoesSoftwareExistsAsync(request.SoftwareId) == false)
        {
            return NotFound("Software with this ID doesn't exist in the database");
        }
        
        if(_saleService.IsSupportPeriodValid(request.AdditionalSupportPeriod) == false)
        {
            return BadRequest("Support period is invalid. Must be between 1 year and 4 years");
        }

        if (_saleService.IsExpirationDateValid(request.ExpiresAt) == false)
        {
            return BadRequest("Expiration date is invalid. Must be between 3 and 30 days from now");
        }

        bool isPrev = await _clientService.IsPrevClientAsync(request.ClientId);

        decimal totalPrice = await _softwareService.CalculatePriceAsync(request, isPrev);
        
        var sale = await _saleService.PrepareNewSale(request, totalPrice);
        
        return Ok("Sale created");
    }
    
    [AllowAnonymous]
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
            var allPaymentsSum = await _saleService.SumUpAllPaymentsAsync(request.SingleSaleId);

            if (await _saleService.MakeNewPaymentTransactionAsync(request) == false)
            {
                return BadRequest("Payment failed");
            }

            if (await _saleService.UpdateIsPaidParamAsync(request, allPaymentsSum) == false)
            {
                return BadRequest("Payment failed");
            }

            return Ok("Payment created");
        } // 'else'
        
        await _saleService.RollBackPaymentsAsync(request.SingleSaleId);

        SingleSale outdatedSale = await _saleService.DeleteSaleAsync(request.SingleSaleId);
        
        // automatic new sale 
        NewSaleDto newSaleDto = _saleService.PrepareSingleSaleDto(outdatedSale);

        await MakeNewSaleAsync(newSaleDto);
        
        return Ok("Payment was cancelled due to exceeding the payment deadline. All payments were rolled back." +
                  "Sale was deleted. Automatic new sale was created");
    }
}