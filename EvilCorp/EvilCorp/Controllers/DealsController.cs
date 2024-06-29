using EvilCorp.DTOs.DealDTOs;
using EvilCorp.Services.ClientService;
using EvilCorp.Services.DealService;
using EvilCorp.Services.SoftwareService;
using Microsoft.AspNetCore.Mvc;

namespace EvilCorp.Controllers;

[ApiController]
[Route("EvilCorp")]
public class DealsController : ControllerBase
{
    private readonly IDealService _dealService;
    private readonly IClientService _clientService;
    private readonly ISoftwareService _softwareService;
    
    public DealsController(IDealService dealService, IClientService clientService, ISoftwareService softwareService)
    {
        _dealService = dealService;
        _clientService = clientService;
        _softwareService = softwareService;
    }

    [HttpPost("CreateDeal")]
    public async Task<IActionResult> CreateDealAsync([FromBody] NewDealDto request)
    {
        if (await _clientService.DoesClientExistsAsync(request.ClientId) == false)
        {
            return NotFound("Client with this ID doesn't exist in the database");
        }

        if (await _softwareService.DoesSoftwareExistsAsync(request.SoftwareId) == false)
        {
            return NotFound("Software with this ID doesn't exist in the database");
        }
        
        if(_dealService.IsSupportPeriodValid(request.SupportPeriod) == false)
        {
            return BadRequest("Support period is invalid. Must be between 1 year and 4 years");
        }

        if (_dealService.IsExpirationDateValid(request.ExpiresAt) == false)
        {
            return BadRequest("Expiration date is invalid. Must be between 3 and 30 days from now");
        }

        bool isPrev = await _clientService.IsPrevClientAsync(request.ClientId);

        decimal totalPrice = await _softwareService.CalculatePriceAsync(request, isPrev);
        
        await _dealService.CreateDealAsync(request, totalPrice);
        
        return Ok("Deal created");
    }
}