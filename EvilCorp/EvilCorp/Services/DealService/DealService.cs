using EvilCorp.Context;
using EvilCorp.DTOs.DealDTOs;
using EvilCorp.Models;

namespace EvilCorp.Services.DealService;

public class DealService : IDealService
{
    private readonly EvilCorpContext _context;
    
    public DealService(EvilCorpContext context)
    {
        _context = context;
    }

    public bool IsSupportPeriodValid(int period)
    {
        if(period < 1 || period > 4)
        {
            return false;
        }

        return true;
    }

    public bool IsExpirationDateValid(DateTime expirationDate)
    {
        if(expirationDate < DateTime.Now.AddDays(3) || expirationDate > DateTime.Now.AddDays(30))
        {
            return false;
        }
        
        return true;
    }

    public async Task<bool> CreateDealAsync(NewDealDto request, decimal totalPrice)
    {
        SingleSale deal = new SingleSale()
        {
            CreatedAt = DateTime.Now,
            ExpiresAt = request.ExpiresAt,
            Price = totalPrice,
            UpdatesInfo = request.UpdatesInfo,
            SupportPeriod = request.SupportPeriod,
            IsPaid = "N",
            ClientId = request.ClientId,
            SoftwareId = request.SoftwareId
        };
        
        await _context.SingleSale.AddAsync(deal);
        
        await _context.SaveChangesAsync();

        return true;
    }
}