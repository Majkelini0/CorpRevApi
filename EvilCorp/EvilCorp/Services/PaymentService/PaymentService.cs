using EvilCorp.Context;
using EvilCorp.DTOs.PaymentDTOs;
using EvilCorp.Models;
using Microsoft.EntityFrameworkCore;

namespace EvilCorp.Services.PaymentService;

public class PaymentService : IPaymentService
{
    private readonly EvilCorpContext _context;
    
    public PaymentService(EvilCorpContext context)
    {
        _context = context;
    }

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

    public async Task<bool> UpdateIfPaidParamAsync(NewPaymentDto request ,decimal allPaymentsSum)
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
}