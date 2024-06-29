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
}