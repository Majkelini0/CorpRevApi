using EvilCorp.Services.DealService;
using Moq;

namespace EvilCorpTests.Mocks;

public class SaleMock
{
    public Mock<ISaleService> _saleMock;

    public SaleMock()
    {
        _saleMock = new Mock<ISaleService>();
    }
}