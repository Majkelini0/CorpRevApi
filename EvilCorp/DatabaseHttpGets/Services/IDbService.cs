using DatabaseHttpGets.DTOs;
using DatabaseHttpGets.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseHttpGets.Services;

public interface IDbService
{
    public Task<List<Individual>> DisplayIndividualsAsync();
    
    public Task<List<Company>> DisplayCompaniesAsync();
    
    public Task<List<Client>> DisplayClientsAsync();
    
    public Task<List<SingleSale>> DisplaySingleSalesAsync();
    
    public Task<List<Payment>> DisplayPaymentsAsync();
    
    public Task<List<Software>> DisplaySoftwaresAsync();
    
    public Task<List<Discount>> DisplayDiscountsAsync();
    
    public Task<List<AvailableDiscountDto>> DisplayAvailableDiscountsAsync();
    
    public Task<List<User>> DisplayUsersAsync();
}