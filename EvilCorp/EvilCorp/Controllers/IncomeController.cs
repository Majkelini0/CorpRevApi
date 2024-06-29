using EvilCorp.Services.IncomeService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvilCorp.Controllers;

[ApiController]
[Route("EvilCorp/[controller]")]
public class IncomeController : ControllerBase
{
    private readonly IIncomeService _incomeService;
    
    public IncomeController(IIncomeService incomeService)
    {
        _incomeService = incomeService;
    }

    [AllowAnonymous]
    [HttpGet("Year/{year:int}")]
    public async Task<IActionResult> CalculateCurrentCompanyIncomeByYearAsync(int year)
    {
        var income = await _incomeService.CalculateCurrentCompanyIncomeByYearAsync(year);

        return Ok("Company income for year " + year + " is: " + income + " PLN");
    }

    [AllowAnonymous]
    [HttpGet("Prognosed/Year/{year:int}")]
    public async Task<IActionResult> CalculatePrognosedCompanyIncomeByYearAsync(int year)
    {
        var income = await _incomeService.CalculatePrognosedCompanyIncomeByYearAsync(year);
        
        return Ok("Prognosed company income for year " + year + " is: " + income + " PLN");
    }
    
    [AllowAnonymous]
    [HttpGet("Year/{year:int}/Product/{name}")]
    public async Task<IActionResult> CalculateCurrentCompanyIncomeByYearByProductAsync(int year, string name)
    {
        var income = await _incomeService.CalculateCurrentCompanyIncomeByYearByProductAsync(year, name);

        return Ok("Company income for year " + year + " for product " + name + " is: " + income + " PLN");
    }
    
    [AllowAnonymous]
    [HttpGet("Prognosed/Year/{year:int}/Product/{name}")]
    public async Task<IActionResult> CalculatePrognosedCompanyIncomeByYearByProductAsync(int year, string name)
    {
        var income = await _incomeService.CalculatePrognosedCompanyIncomeByYearByProductAsync(year, name);

        return Ok("Prognosed company income for year " + year + " for product " + name + " is: " + income + " PLN");
    }
}