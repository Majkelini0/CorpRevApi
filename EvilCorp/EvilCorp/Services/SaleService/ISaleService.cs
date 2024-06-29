using EvilCorp.DTOs.DealDTOs;
using EvilCorp.Models;

namespace EvilCorp.Services.DealService;

public interface ISaleService
{
    public bool IsSupportPeriodValid(int period);
    
    public bool IsExpirationDateValid(DateTime expirationDate);
    
    public Task<SingleSale> PrepareNewSale(NewSaleDto request, decimal totalPrice);
    
    public Task<bool> DoesSaleExistsAsync(int saleId);
    
    public Task<bool> IsSaleAlreadyPaidAsync(int saleId);
    
    public Task<bool> IsSaleStillValidAsync(int id);
    
    public Task<bool> DeleteSaleAsync(int id);
}