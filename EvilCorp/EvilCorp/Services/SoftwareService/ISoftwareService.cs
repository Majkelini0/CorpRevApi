using EvilCorp.DTOs.DealDTOs;

namespace EvilCorp.Services.SoftwareService;

public interface ISoftwareService
{
    public Task<bool> DoesSoftwareExistsAsync(int id);
    
    public Task<decimal> CalculatePriceAsync(NewSaleDto request, bool isPrev);
}