using EvilCorp.Models;

namespace EvilCorp.DTOs.PaymentDTOs;

public class NewPaymentDto
{
    public decimal Amount { get; set; }
    
    public int SingleSaleId { get; set; }
}