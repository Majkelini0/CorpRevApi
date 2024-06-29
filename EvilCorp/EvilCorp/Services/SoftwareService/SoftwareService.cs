using EvilCorp.Context;
using EvilCorp.DTOs.DealDTOs;
using Microsoft.EntityFrameworkCore;

namespace EvilCorp.Services.SoftwareService;

public class SoftwareService : ISoftwareService
{
    private readonly EvilCorpContext _context;
    
    public SoftwareService(EvilCorpContext context)
    {
        _context = context;
    }

    public async Task<bool> DoesSoftwareExistsAsync(int id)
    {
        return await _context.Software.FirstOrDefaultAsync(e => e.IdSoftware == id) != null;
    }

    public async Task<decimal> CalculatePriceAsync(NewDealDto request, bool isPrev)
    {
        var soft = await _context.Software.FirstOrDefaultAsync(e => e.IdSoftware == request.SoftwareId);
        if (soft == null)
        {
            throw new ArgumentException("Software with given id does not exist");
        }
        double softPrice = Decimal.ToDouble(soft.Price);
        
        double additionalCosts = (1000 * request.SupportPeriod) - 1000;

        double prevClientDiscount = 0;
        if (isPrev)
        {
            prevClientDiscount = 0.05;
        }
        
        Console.WriteLine("// // // Prev client discount: " + prevClientDiscount);
        
        double maxDiscount = 0;
        maxDiscount = FindMaxDiscount(request.SoftwareId);

        double totalPrice = 0;
        totalPrice += (additionalCosts + softPrice * (1 - prevClientDiscount - maxDiscount));

        return Convert.ToDecimal(totalPrice);
    }

    private double FindMaxDiscount(int SoftwareId)
    {
        double maxDiscount = 0;

        var discounts = _context.AvailableDiscount
            .Include(x => x.Discount);

        foreach (var discount in discounts)
        {
            if (discount.SoftwareId != SoftwareId)
            {
                continue;
            }
            
            if(DateTime.Now < discount.Discount.StartDate || DateTime.Now > discount.Discount.EndDate)
            {
                continue;
            }
            
            if (Decimal.ToDouble(discount.Discount.Value) > maxDiscount)
            {
                maxDiscount = Decimal.ToDouble(discount.Discount.Value);
            }
        }
        maxDiscount = maxDiscount / 100;
        
        Console.WriteLine("// // // Max discount: " + maxDiscount); //////////////////////////////////////////////////

        return maxDiscount;
    }
}