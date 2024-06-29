using EvilCorp.DTOs.DealDTOs;

namespace EvilCorp.Services.DealService;

public interface IDealService
{
    public bool IsSupportPeriodValid(int period);
    
    public bool IsExpirationDateValid(DateTime expirationDate);
    
    public Task<bool> CreateDealAsync(NewDealDto request, decimal totalPrice);
}