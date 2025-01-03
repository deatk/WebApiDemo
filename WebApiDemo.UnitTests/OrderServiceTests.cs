using Moq;
using Xunit;
using WebApiDemoServices;
using WebApiDemoRepositories.Interfaces;
using WebApiDemoModels;

public class OrderServiceTests
{
    private readonly Mock<IOrderRepository> _mockRepository;
    private readonly OrderService _orderService;

    public OrderServiceTests()
    {
        _mockRepository = new Mock<IOrderRepository>();
        _orderService = new OrderService(_mockRepository.Object);
    }

    
}
