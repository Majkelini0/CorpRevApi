using DatabaseHttpGets.Context;
using DatabaseHttpGets.DTOs;
using DatabaseHttpGets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseHttpGets.Services;

public class DbService : IDbService
{
    private readonly EvilCorpContext _context;
    
    public DbService(EvilCorpContext context)
    {
        _context = context;
    }

    public async Task<List<Individual>> DisplayIndividualsAsync()
    {
        return await _context.Individuals.ToListAsync();
    }

    public async Task<List<Company>> DisplayCompaniesAsync()
    {
        return await _context.Companies.ToListAsync();
    }

    public async  Task<List<Client>> DisplayClientsAsync()
    {
        return await _context.Clients.ToListAsync();
    }

    public async  Task<List<SingleSale>> DisplaySingleSalesAsync()
    {
        return await _context.SingleSales.ToListAsync();
    }

    public async  Task<List<Payment>> DisplayPaymentsAsync()
    {
        return await _context.Payments.ToListAsync();
    }

    public async  Task<List<Software>> DisplaySoftwaresAsync()
    {
        return await _context.Softwares.ToListAsync();
    }

    public async  Task<List<Discount>> DisplayDiscountsAsync()
    {
        return await _context.Discounts.ToListAsync();
    }

    public async Task<List<AvailableDiscountDto>> DisplayAvailableDiscountsAsync()
    {
        var r = await _context.Softwares.Include(x => x.Discounts).ToListAsync();

        var list = new List<AvailableDiscountDto>();
        
        foreach (var software in r)
        {
            foreach (var discount in software.Discounts)
            {
                list.Add(new AvailableDiscountDto()
                {
                    IdSoftware = software.IdSoftware,
                    IdDiscount = discount.IdDiscount
                });
            }
        }

        return list;
    }

    public async  Task<List<User>> DisplayUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }
}