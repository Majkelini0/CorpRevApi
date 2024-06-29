using EvilCorp.Controllers;
using EvilCorp.DTOs.ClientDTOs;
using EvilCorp.Services.ClientService;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EvilCorpTests.Tests;

public class ClientTests
{
    private Mock<IClientService> _clientMock;
    private ClientsController _controller;

    public ClientTests()
    {
        _clientMock = new Mock<IClientService>();
        _controller = new ClientsController(_clientMock.Object);
    }

    //1. AddNewIndividual
    [Fact]
    public async Task AddNewIndividual_InvalidPesel()
    {
        var request= new NewIndividualDto { Pesel = "invalid" };
        var result= await _controller.AddNewIndividualAsync(request);
        Assert.IsType<BadRequestObjectResult>(result);
    }
    [Fact]
    public async Task AddNewIndividual_PeselIsInDatabase()
    {
        var request= new NewIndividualDto { Pesel = "12345678900" };
        _clientMock.Setup(s => s.DoesIndividualExistsAsync(request.Pesel)).ReturnsAsync(true);
        var result= await _controller.AddNewIndividualAsync(request);
        Assert.IsType<BadRequestObjectResult>(result);
    }
    
    //2. AddNewCompany
    [Fact]
    public async Task AddNewCompany_KrsInvalid()
    {
        var request = new NewCompanyDto { Krs = "invalid" };
        var result = await _controller.AddNewCompanyAsync(request);
        Assert.IsType<BadRequestObjectResult>(result);
    }
    [Fact]
    public async Task AddNewCompany_KrsIsInDatabase()
    {
        var request = new NewCompanyDto { Krs = "1234567890" };
        _clientMock.Setup(s => s.DoesCompanyExistsAsync(request.Krs)).ReturnsAsync(true);
        var result = await _controller.AddNewCompanyAsync(request);
        Assert.IsType<BadRequestObjectResult>(result);
    }
    

    //3. DeleteClient
    [Fact]
    public async Task DeleteIndividual_NoIndividualWithThisID()
    {
        _clientMock.Setup(s => s.DoesIndividualExistsAsync(It.IsAny<int>())).ReturnsAsync(false);
        var result = await _controller.DeleteIndividualAsync(1);
        Assert.IsType<NotFoundObjectResult>(result);
    }

    //4. UpdateIndividual
    [Fact]
    public async Task UpdateIndividual_NoIndividualWithThisID()
    {
        _clientMock.Setup(s => s.DoesIndividualExistsAsync(It.IsAny<int>())).ReturnsAsync(false);
        var request = new UpdateIndividualDto();
        var result = await _controller.UpdateIndividualAsync(request, 1);
        Assert.IsType<NotFoundObjectResult>(result);
    }
    //5. UpdateCompany

    [Fact]
    public async Task UpdateCompany_NoCompanyWithThisId()
    {
        _clientMock.Setup(s => s.DoesCompanyExistsAsync(It.IsAny<int>())).ReturnsAsync(false);
        var request = new UpdateCompanyDto();
        var result = await _controller.UpdateCompanyAsync(request, 1);
        Assert.IsType<NotFoundObjectResult>(result);
    }
}