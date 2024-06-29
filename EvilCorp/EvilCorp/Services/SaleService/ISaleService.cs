﻿using EvilCorp.DTOs.DealDTOs;
using EvilCorp.Models;

namespace EvilCorp.Services.DealService;

public interface ISaleService
{
    public bool IsSupportPeriodValid(int period);
    
    public bool IsExpirationDateValid(DateTime expirationDate);
    
    public Task<SingleSale> NewSaleAsync(NewSaleDto request, decimal totalPrice);
    
    public Task<bool> DoesSaleExistsAsync(int saleId);
    
    public Task<bool> IsSaleAlreadyPaidAsync(int saleId);

}