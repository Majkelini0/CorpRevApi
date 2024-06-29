namespace EvilCorp.Services.IncomeService;

public interface IIncomeService
{
    public Task<decimal> CalculateCurrentCompanyIncomeByYearAsync(int year);
    
    public Task<decimal> CalculatePrognosedCompanyIncomeByYearAsync(int year);
}