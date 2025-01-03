using Moq;
using Xunit;
using WebApiDemoServices;
using WebApiDemoRepositories.Interfaces;
using WebApiDemoModels;

public class PizzaServiceTests
{
    private readonly Mock<IPizzaRepository> _mockRepository;
    private readonly PizzaService _pizzaService;

    public PizzaServiceTests()
    {
        _mockRepository = new Mock<IPizzaRepository>();
        _pizzaService = new PizzaService(_mockRepository.Object);
    }

    
}
