namespace EvilCorp.Services.IncomeService;

public interface IIncomeService
{
    public Task<decimal> CalculateCompanyIncomeByYearAsync(int year);
}