using System.Collections;
using DatabaseHttpGets.Services;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseHttpGets.Controllers;

[ApiController]
[Route("EvilCorp/[controller]")]
public class DbController : ControllerBase
{
    private readonly IDbService _dbService;
    
    public DbController(IDbService dbService)
    {
        _dbService = dbService;
    }
    
    [HttpGet("Individuals")]
    public async Task<IActionResult> DisplayIndividualsAsync()
    {
        return Ok(await _dbService.DisplayIndividualsAsync());
    }

    [HttpGet("Companies")]
    public async Task<IActionResult> DisplayCompaniesAsync()
    {
        return Ok(await _dbService.DisplayCompaniesAsync());
    }
    
    [HttpGet("Clients")]
    public async Task<IActionResult> DisplayClientsAsync()
    {
        return Ok(await _dbService.DisplayClientsAsync());
    }
    
    [HttpGet("SingleSales")]
    public async Task<IActionResult> DisplaySingleSalesAsync()
    {
        return Ok(await _dbService.DisplaySingleSalesAsync());
    }
    
    [HttpGet("Payments")]
    public async Task<IActionResult> DisplayPaymentsAsync()
    {
        return Ok(await _dbService.DisplayPaymentsAsync());
    }
    
    [HttpGet("Softwares")]
    public async Task<IActionResult> DisplaySoftwaresAsync()
    {
        return Ok(await _dbService.DisplaySoftwaresAsync());
    }
    
    [HttpGet("Discounts")]
    public async Task<IActionResult> DisplayDiscountsAsync()
    {
        return Ok(await _dbService.DisplayDiscountsAsync());
    }
    
    [HttpGet("AvailableDiscounts")]
    public async Task<IActionResult> DisplayAvailableDiscountsAsync()
    {
        return Ok(await _dbService.DisplayAvailableDiscountsAsync());
    }
    
    [HttpGet("Users")]
    public async Task<IActionResult> DisplayUsersAsync()
    {
        return Ok(await _dbService.DisplayUsersAsync());
    }
    
}