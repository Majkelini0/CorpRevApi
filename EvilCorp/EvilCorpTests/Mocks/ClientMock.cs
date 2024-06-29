using EvilCorp.Services.ClientService;
using Moq;

namespace EvilCorpTests.Mocks;

public class ClientMock
{
    private readonly Mock<IClientService> _clientMock;

    public ClientMock()
    {
        _clientMock = new Mock<IClientService>();
    }

    // [Fact]
    // public async Task TestMethod()
    // {
    //     var peselmock = "12345678900";
    //     _clientMock.Setup(service => service.DoesIndividualExistsAsync(peselmock)).ReturnsAsync(true);
    //         
    //     var result = await _clientMock.Object.DoesIndividualExistsAsync(peselmock);
    //
    //     Assert.True(result);
    // }
}