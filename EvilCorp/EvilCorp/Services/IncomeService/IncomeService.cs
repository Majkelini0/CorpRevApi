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

    public async Task<decimal> CalculateCompanyIncomeByYearAsync(int year)
    {
        var sum = await _context.SingleSale
            .Where(e => e.FulfilledAt != null && e.FulfilledAt.Value.Year == year)
            .SumAsync(x => x.Price);

        return sum;
    }
}