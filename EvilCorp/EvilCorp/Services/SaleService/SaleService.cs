using EvilCorp.Context;
using EvilCorp.DTOs.DealDTOs;
using EvilCorp.Models;
using Microsoft.EntityFrameworkCore;

namespace EvilCorp.Services.DealService;

public class SaleService : ISaleService
{
    private readonly EvilCorpContext _context;
    
    public SaleService(EvilCorpContext context)
    {
        _context = context;
    }

    public bool IsSupportPeriodValid(int period)
    {
        if(period < 0 || period > 3)
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

    public async Task<SingleSale> PrepareNewSale(NewSaleDto request, decimal totalPrice)
    {
        SingleSale deal = new SingleSale()
        {
            CreatedAt = DateTime.Now,
            ExpiresAt = request.ExpiresAt,
            Price = totalPrice,
            UpdatesInfo = request.UpdatesInfo,
            AdditionalSupportPeriod = request.AdditionalSupportPeriod,
            IsPaid = "N",
            ClientId = request.ClientId,
            SoftwareId = request.SoftwareId
        };
        
        await _context.SingleSale.AddAsync(deal);
        
        await _context.SaveChangesAsync();

        return deal;
    }

    public async Task<bool> DoesSaleExistsAsync(int saleId)
    {
        return await _context.SingleSale.FirstOrDefaultAsync(e => e.IdSale == saleId) != null;
    }

    public async Task<bool> IsSaleAlreadyPaidAsync(int saleId)
    {
        var sale = await _context.SingleSale.FirstOrDefaultAsync(e => e.IdSale == saleId);
        if (sale.IsPaid == "Y")
        {
            return true;
        }

        return false;
    }
    
    public async Task<bool> IsSaleStillValidAsync(int id)
    {
        var sale = await _context.SingleSale
            .Where(e => e.IdSale == id)
            .FirstOrDefaultAsync();
        
        if(sale.ExpiresAt < DateTime.Now)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> DeleteSaleAsync(int id)
    {
        await _context.SingleSale.Where(e => e.IdSale == id).ForEachAsync(e => _context.SingleSale.Remove(e));

        // _context.SingleSale.Where(e => e.IdSale == id).Select(e => _context.SingleSale.Remove(e));
        
        await _context.SaveChangesAsync();

        return true;
    }
}