using EvilCorp.Context;
using EvilCorp.DTOs.DealDTOs;
using EvilCorp.DTOs.PaymentDTOs;
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

    public async Task<SingleSale> DeleteSaleAsync(int id)
    {
        var outdatedSale = await _context.SingleSale
            .Where(e => e.IdSale == id)
            .FirstOrDefaultAsync();
        
        await _context.SingleSale.Where(e => e.IdSale == id).ForEachAsync(e => _context.SingleSale.Remove(e));
        
        await _context.SaveChangesAsync();

        return outdatedSale;
    }

    public NewSaleDto PrepareSingleSaleDto(SingleSale outdatedSale)
    {
        NewSaleDto newSaleDto = new NewSaleDto()
        {
            ExpiresAt = DateTime.Now.AddDays(14),
            UpdatesInfo = outdatedSale.UpdatesInfo,
            AdditionalSupportPeriod = outdatedSale.AdditionalSupportPeriod,
            ClientId = outdatedSale.ClientId,
            SoftwareId = outdatedSale.SoftwareId
        };

        return newSaleDto;
    }

    // // //
    
    public async Task<decimal> SumUpAllPaymentsAsync(int id)
    {
        return await _context.Payment.Where(e => e.SingleSaleId == id).SumAsync(x => x.Amount);
    }

    public async Task<bool> MakeNewPaymentTransactionAsync(NewPaymentDto request)
    {
        await _context.Payment.AddAsync(new Payment()
            {
                Amount = request.Amount,
                SingleSaleId = request.SingleSaleId
            }
        );
        
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> UpdateIsPaidParamAsync(NewPaymentDto request ,decimal allPaymentsSum)
    {
        var sale = await _context.SingleSale
            .Where(e => e.IdSale == request.SingleSaleId).FirstOrDefaultAsync();

        if (sale == null)
        {
            return false;
        }

        var price = sale.Price;

        if (allPaymentsSum + request.Amount >= price)
        {
            sale.IsPaid = "Y";
            sale.FulfilledAt = DateTime.Now;
            
            var client = await _context.Client
                .Where(e => e.IdClient == sale.ClientId)
                .FirstOrDefaultAsync();

            if (client == null)
            {
                return false;
            }

            client.PrevClient = "Y";
        }
        
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RollBackPaymentsAsync(int id)
    {
        await _context.Payment.Where(e => e.SingleSaleId == id).ForEachAsync(e => _context.Payment.Remove(e));

        await _context.SaveChangesAsync();

        return true;
    }
}