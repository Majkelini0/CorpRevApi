namespace EvilCorp.Services.IncomeService;

public interface IIncomeService
{
    public Task<decimal> CalculateCurrentCompanyIncomeByYearAsync(int year);
    
    public Task<decimal> CalculatePrognosedCompanyIncomeByYearAsync(int year);

    public Task<decimal> CalculateCurrentCompanyIncomeByYearByProductAsync(int year, string name);
    
    public Task<decimal> CalculatePrognosedCompanyIncomeByYearByProductAsync(int year, string name);
}