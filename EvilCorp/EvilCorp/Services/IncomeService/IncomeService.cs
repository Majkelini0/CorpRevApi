using EvilCorp.Context;
using Microsoft.EntityFrameworkCore;

namespace EvilCorp.Services.IncomeService;

public class IncomeService : IIncomeService
{
    private readonly EvilCorpContext _context;
    
    public IncomeService(EvilCorpContext context)
    {
        _context = context;
    }

    public async Task<decimal> CalculateCurrentCompanyIncomeByYearAsync(int year)
    {
        var sum = await _context.SingleSale
            .Where(e => e.FulfilledAt != null && e.FulfilledAt.Value.Year == year)
            .SumAsync(x => x.Price);

        return sum;
    }

    public async Task<decimal> CalculatePrognosedCompanyIncomeByYearAsync(int year)
    {
        var sum = await _context.SingleSale
            .Where(e => e.FulfilledAt != null && e.FulfilledAt.Value.Year == year)
            .SumAsync(x => x.Price);
        
        sum += await _context.SingleSale
            .Where(e => e.ExpiresAt.Year == year && e.IsPaid == "N")
            .SumAsync(x => x.Price * 0.667m);

        return sum;
    }
    
    public async Task<decimal> CalculateCurrentCompanyIncomeByYearByProductAsync(int year, string name)
    {
        var sum = await _context.Software
            .Where(x => x.Name == name)
            .SelectMany(x => x.SingleSales)
            .Where(z => z.FulfilledAt != null && z.FulfilledAt.Value.Year == year)
            .SumAsync(y => y.Price);

        return sum;
    }
}