using EvilCorp.Helpers;
using EvilCorp.Services.IncomeService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvilCorp.Controllers;

[ApiController]
[Route("EvilCorp/[controller]")]
public class IncomeController : ControllerBase
{
    private readonly IIncomeService _incomeService;
    private readonly ExchangeRateHelper _exchangeRateHelper;
    
    public IncomeController(IIncomeService incomeService, ExchangeRateHelper exchangeRateHelper)
    {
        _incomeService = incomeService;
        _exchangeRateHelper = exchangeRateHelper;
    }

    [AllowAnonymous]
    [HttpGet("Year/{year:int}")]
    public async Task<IActionResult> CalculateCurrentCompanyIncomeByYearAsync(int year, string countryCode = "PLN")
    {
        countryCode = countryCode.ToUpper();
        var income = await _incomeService.CalculateCurrentCompanyIncomeByYearAsync(year);

        income = await _exchangeRateHelper.GetExchangedIncome(income, countryCode);

        return Ok("Company income for year " + year + " is: " + income + " " + countryCode);
    }

    [AllowAnonymous]
    [HttpGet("Prognosed/Year/{year:int}")]
    public async Task<IActionResult> CalculatePrognosedCompanyIncomeByYearAsync(int year, string countryCode = "PLN")
    {
        countryCode = countryCode.ToUpper();
        var income = await _incomeService.CalculatePrognosedCompanyIncomeByYearAsync(year);
        
        income = await _exchangeRateHelper.GetExchangedIncome(income, countryCode);
        
        return Ok("Prognosed company income for year " + year + " is: " + income + " " + countryCode);
    }
    
    [AllowAnonymous]
    [HttpGet("Year/{year:int}/Product/{name}")]
    public async Task<IActionResult> CalculateCurrentCompanyIncomeByYearByProductAsync(int year, string name, string countryCode = "PLN")
    {
        countryCode = countryCode.ToUpper();
        var income = await _incomeService.CalculateCurrentCompanyIncomeByYearByProductAsync(year, name);
        
        income = await _exchangeRateHelper.GetExchangedIncome(income, countryCode);

        return Ok("Company income for year " + year + " for product " + name + " is: " + income + " " + countryCode);
    }
    
    [AllowAnonymous]
    [HttpGet("Prognosed/Year/{year:int}/Product/{name}")]
    public async Task<IActionResult> CalculatePrognosedCompanyIncomeByYearByProductAsync(int year, string name, string countryCode = "PLN")
    {
        countryCode = countryCode.ToUpper();
        var income = await _incomeService.CalculatePrognosedCompanyIncomeByYearByProductAsync(year, name);
        
        income = await _exchangeRateHelper.GetExchangedIncome(income, countryCode);

        return Ok("Prognosed company income for year " + year + " for product " + name + " is: " + income + " " + countryCode);
    }
}