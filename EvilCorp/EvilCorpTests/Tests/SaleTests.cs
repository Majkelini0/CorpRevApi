using EvilCorp.Controllers;
using EvilCorp.DTOs.DealDTOs;
using EvilCorp.DTOs.PaymentDTOs;
using EvilCorp.Services.ClientService;
using EvilCorp.Services.SoftwareService;
using EvilCorpTests.Mocks;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EvilCorpTests.Tests;

public class SaleTests
{
    private readonly SalesController _salesController;
    private readonly SaleMock _saleMock;
    private readonly Mock<IClientService> _clientMock;
    private readonly Mock<ISoftwareService> _softwareMock;

    public SaleTests()
    {
        _saleMock = new SaleMock();
        _clientMock = new Mock<IClientService>();
        _softwareMock = new Mock<ISoftwareService>();
        _salesController = new SalesController(_saleMock._saleMock.Object, _clientMock.Object, _softwareMock.Object);
    }

    //1. MakeNewSale
    [Fact]
    public async Task MakeNewSale_NoClientId()
    {
        var newSaleDto = new NewSaleDto { ClientId = 1, SoftwareId = 1, AdditionalSupportPeriod = 1, ExpiresAt = DateTime.Now.AddDays(5) };
        _clientMock.Setup(x => x.DoesClientExistsAsync(It.IsAny<int>())).ReturnsAsync(false);
        
        var result = await _salesController.MakeNewSaleAsync(newSaleDto);
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task MakeNewSale_NoSoftwareId()
    {
        var newSaleDto = new NewSaleDto { ClientId = 1, SoftwareId = 1, AdditionalSupportPeriod = 1, ExpiresAt = DateTime.Now.AddDays(5) };
        _clientMock.Setup(x => x.DoesClientExistsAsync(It.IsAny<int>())).ReturnsAsync(true);
        _softwareMock.Setup(x => x.DoesSoftwareExistsAsync(It.IsAny<int>())).ReturnsAsync(false);
        
        var result = await _salesController.MakeNewSaleAsync(newSaleDto);
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task MakeNewSale_PeriodInvalid()
    {
        var newSaleDto = new NewSaleDto { ClientId = 1, SoftwareId = 1, AdditionalSupportPeriod = 1, ExpiresAt = DateTime.Now.AddDays(5) };
        _clientMock.Setup(x => x.DoesClientExistsAsync(It.IsAny<int>())).ReturnsAsync(true);
        _softwareMock.Setup(x => x.DoesSoftwareExistsAsync(It.IsAny<int>())).ReturnsAsync(true);
        _saleMock._saleMock.Setup(x => x.IsSupportPeriodValid(It.IsAny<int>())).Returns(false);
        
        var result = await _salesController.MakeNewSaleAsync(newSaleDto);
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task MakeNewSale_ExpirationDateInvalid()
    {
        var newSaleDto = new NewSaleDto { ClientId = 1, SoftwareId = 1, AdditionalSupportPeriod = 1, ExpiresAt = DateTime.Now.AddDays(5) };
        _clientMock.Setup(x => x.DoesClientExistsAsync(It.IsAny<int>())).ReturnsAsync(true);
        _softwareMock.Setup(x => x.DoesSoftwareExistsAsync(It.IsAny<int>())).ReturnsAsync(true);
        _saleMock._saleMock.Setup(x => x.IsSupportPeriodValid(It.IsAny<int>())).Returns(true);
        _saleMock._saleMock.Setup(x => x.IsExpirationDateValid(It.IsAny<DateTime>())).Returns(false);
        
        var result = await _salesController.MakeNewSaleAsync(newSaleDto);
        Assert.IsType<BadRequestObjectResult>(result);
    }
    
    //2. MakeNewPayment
    [Fact]
    public async Task MakeNewPayment_NoPaymentId()
    { 
        var newPaymentDto = new NewPaymentDto { SingleSaleId = 1, Amount = 333 };
        _saleMock._saleMock.Setup(x => x.DoesSaleExistsAsync(It.IsAny<int>())).ReturnsAsync(false);
        
        var result = await _salesController.MakeNewPaymentAsync(newPaymentDto);
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task MakeNewPayment_Paid()
    {
        var newPaymentDto = new NewPaymentDto { SingleSaleId = 1, Amount = 333 };
        _saleMock._saleMock.Setup(x => x.DoesSaleExistsAsync(It.IsAny<int>())).ReturnsAsync(true);
        _saleMock._saleMock.Setup(x => x.IsSaleAlreadyPaidAsync(It.IsAny<int>())).ReturnsAsync(true);
        
        var result = await _salesController.MakeNewPaymentAsync(newPaymentDto);
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task MakeNewPayment_ValidSaleFailedPayment()
    {
        var newPaymentDto = new NewPaymentDto { SingleSaleId = 1, Amount = 333 };
        _saleMock._saleMock.Setup(x => x.DoesSaleExistsAsync(It.IsAny<int>())).ReturnsAsync(true);
        _saleMock._saleMock.Setup(x => x.IsSaleAlreadyPaidAsync(It.IsAny<int>())).ReturnsAsync(false);
        _saleMock._saleMock.Setup(x => x.IsSaleStillValidAsync(It.IsAny<int>())).ReturnsAsync(true);
        _saleMock._saleMock.Setup(x => x.MakeNewPaymentTransactionAsync(It.IsAny<NewPaymentDto>())).ReturnsAsync(false);
        
        var result = await _salesController.MakeNewPaymentAsync(newPaymentDto);
        Assert.IsType<BadRequestObjectResult>(result);
    }
    [Fact]
    public async Task MakeNewPayment_ValidSaleFailedPayment2()
    {
        var newPaymentDto = new NewPaymentDto { SingleSaleId = 1, Amount = 333 };
        _saleMock._saleMock.Setup(x => x.DoesSaleExistsAsync(It.IsAny<int>())).ReturnsAsync(true);
        _saleMock._saleMock.Setup(x => x.IsSaleAlreadyPaidAsync(It.IsAny<int>())).ReturnsAsync(false);
        _saleMock._saleMock.Setup(x => x.IsSaleStillValidAsync(It.IsAny<int>())).ReturnsAsync(true);
        _saleMock._saleMock.Setup(x => x.MakeNewPaymentTransactionAsync(It.IsAny<NewPaymentDto>())).ReturnsAsync(true);
        _saleMock._saleMock.Setup(x => x.UpdateIsPaidParamAsync(It.IsAny<NewPaymentDto>(), It.IsAny<decimal>())).ReturnsAsync(false);
        
        var result = await _salesController.MakeNewPaymentAsync(newPaymentDto);
        Assert.IsType<BadRequestObjectResult>(result);
    }
}