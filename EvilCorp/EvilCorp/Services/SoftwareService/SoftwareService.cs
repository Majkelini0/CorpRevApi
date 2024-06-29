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
        decimal softPrice = soft.Price;
        Console.WriteLine("// // // Software price: " + softPrice);
        
        decimal additionalCosts = 1000 * request.SupportPeriod - 1000;

        decimal prevClientDiscount = 0;
        if (isPrev)
        {
            prevClientDiscount = (decimal)0.05;
        }
        
        Console.WriteLine("// // // Prev client discount: " + prevClientDiscount);
        
        // check any discounts
        decimal maxDiscount = 0;
        maxDiscount = FindMaxDiscount(request.SoftwareId);

        decimal totalPrice = 0;
        totalPrice += (additionalCosts + softPrice * (1 - prevClientDiscount - maxDiscount));

        return totalPrice;
    }

    private decimal FindMaxDiscount(int SoftwareId)
    {
        decimal maxDiscount = 0;

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
            
            if (discount.Discount.Value > maxDiscount)
            {
                maxDiscount = discount.Discount.Value;
            }
        }
        
        Console.WriteLine("// // // Max discount: " + maxDiscount); //////////////////////////////////////////////////

        return maxDiscount;
    }
}