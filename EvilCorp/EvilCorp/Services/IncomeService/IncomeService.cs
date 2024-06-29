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
    { // thesis ?: only two thirds of the contracts will be signed and paid for
        
        var sum = await _context.SingleSale
            .Where(e => e.FulfilledAt != null && e.FulfilledAt.Value.Year == year)
            .SumAsync(x => x.Price);
        
        sum += await _context.SingleSale
            .Where(e => e.ExpiresAt != null && e.ExpiresAt.Year == year)
            .SumAsync(x => x.Price * 0.66m);

        return sum;
    }
}