using EvilCorp.DTOs.PaymentDTOs;

namespace EvilCorp.Services.PaymentService;

public interface IPaymentService
{
    public Task<decimal> SumUpAllPaymentsAsync(int id);
    
    public Task<bool> MakeNewPaymentTransactionAsync(NewPaymentDto request);
    
    public Task<bool> UpdateIfPaidParamAsync(NewPaymentDto request, decimal allPaymentsSum);
}