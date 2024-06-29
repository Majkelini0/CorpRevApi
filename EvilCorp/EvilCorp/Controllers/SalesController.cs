using EvilCorp.DTOs.DealDTOs;
using EvilCorp.Models;
using EvilCorp.Services.ClientService;
using EvilCorp.Services.DealService;
using EvilCorp.Services.SoftwareService;
using Microsoft.AspNetCore.Mvc;

namespace EvilCorp.Controllers;

[ApiController]
[Route("EvilCorp")]
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

    [HttpPost("NewSale")]
    public async Task<IActionResult> NewSaleAsync([FromBody] NewSaleDto request)
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
        
        var sale = await _saleService.NewSaleAsync(request, totalPrice);
        
        return Ok("Sale created");
    }
}