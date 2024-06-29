using EvilCorp.DTOs.DealDTOs;
using EvilCorp.DTOs.PaymentDTOs;
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
    
    public Task<SingleSale> DeleteSaleAsync(int id);

    public NewSaleDto PrepareSingleSaleDto(SingleSale sale);
    
    // // //
    
    public Task<decimal> SumUpAllPaymentsAsync(int id);
    
    public Task<bool> MakeNewPaymentTransactionAsync(NewPaymentDto request);
    
    public Task<bool> UpdateIsPaidParamAsync(NewPaymentDto request, decimal allPaymentsSum);
    
    public Task<bool> RollBackPaymentsAsync(int id);
}