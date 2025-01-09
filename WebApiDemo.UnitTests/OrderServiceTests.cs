using Moq;
using WebApiDemoModels;
using WebApiDemoModels.Enums;
using WebApiDemoRepositories.Interfaces;
using WebApiDemoServices;

public class OrderServiceTests
{
    private readonly Mock<IOrderRepository> _mockOrderRepository;
    private readonly Mock<IPizzaRepository> _mockPizzaRepository;
    private readonly OrderService _orderService;

    public OrderServiceTests()
    {
        _mockOrderRepository = new Mock<IOrderRepository>();
        _mockPizzaRepository = new Mock<IPizzaRepository>();
        _orderService = new OrderService(_mockOrderRepository.Object, _mockPizzaRepository.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllOrders()
    {
        // Arrange
        var orders = new List<Order>
        {
            new Order { Id = "1", ContactId = "C1", Total = 100 },
            new Order { Id = "2", ContactId = "C2", Total = 200 }
        };
        _mockOrderRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(orders);

        // Act
        var result = await _orderService.GetAllAsync();

        // Assert
        Assert.Equal(orders.Count, result.Count());
        Assert.Equal(orders, result);
        _mockOrderRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnOrder_WhenOrderExists()
    {
        // Arrange
        var order = new Order { Id = "1", ContactId = "C1", Total = 100 };
        _mockOrderRepository.Setup(repo => repo.GetByIdAsync("1")).ReturnsAsync(order);

        // Act
        var result = await _orderService.GetByIdAsync("1");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(order, result);
        _mockOrderRepository.Verify(repo => repo.GetByIdAsync("1"), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenOrderDoesNotExist()
    {
        // Arrange
        _mockOrderRepository.Setup(repo => repo.GetByIdAsync("1")).ReturnsAsync((Order)null);

        // Act
        var result = await _orderService.GetByIdAsync("1");

        // Assert
        Assert.Null(result);
        _mockOrderRepository.Verify(repo => repo.GetByIdAsync("1"), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_ShouldAssignIdAndDate_AndCalculateTotal()
    {
        // Arrange
        var order = new Order
        {
            ContactId = "C1",
            OrderDetails = new List<OrderDetail>
            {
                new OrderDetail { PizzaName = "Margherita", Quantity = 2 }
            },
            Status = OrderStatus.Pending
        };

        var pizza = new Pizza { Name = "Margherita", Price = 10 };
        _mockPizzaRepository.Setup(repo => repo.GetByNameAsync("Margherita")).ReturnsAsync(pizza);
        _mockOrderRepository.Setup(repo => repo.CreateAsync(It.IsAny<Order>())).ReturnsAsync((Order o) => o);

        // Act
        var result = await _orderService.CreateAsync(order);

        // Assert
        Assert.NotNull(result.Id);
        Assert.NotEqual(default, result.OrderDate);
        Assert.Equal(20, result.Total); // 2 * 10 = 20
        _mockOrderRepository.Verify(repo => repo.CreateAsync(order), Times.Once);
        _mockPizzaRepository.Verify(repo => repo.GetByNameAsync("Margherita"), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateOrder_WhenOrderExists()
    {
        // Arrange
        var existingOrder = new Order { Id = "1", ContactId = "C1", Total = 100 };
        var updatedOrder = new Order { Id = "1", ContactId = "C1", Total = 150, OrderDetails = new List<OrderDetail>() };

        _mockOrderRepository.Setup(repo => repo.GetByIdAsync("1")).ReturnsAsync(existingOrder);
        _mockOrderRepository.Setup(repo => repo.UpdateAsync(updatedOrder)).ReturnsAsync(updatedOrder);

        // Act
        var result = await _orderService.UpdateAsync("1", updatedOrder);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(updatedOrder.Total, result.Total);
        _mockOrderRepository.Verify(repo => repo.GetByIdAsync("1"), Times.Once);
        _mockOrderRepository.Verify(repo => repo.UpdateAsync(updatedOrder), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnNull_WhenOrderDoesNotExist()
    {
        // Arrange
        var updatedOrder = new Order { Id = "1", ContactId = "C1", Total = 150 };
        _mockOrderRepository.Setup(repo => repo.GetByIdAsync("1")).ReturnsAsync((Order)null);

        // Act
        var result = await _orderService.UpdateAsync("1", updatedOrder);

        // Assert
        Assert.Null(result);
        _mockOrderRepository.Verify(repo => repo.GetByIdAsync("1"), Times.Once);
        _mockOrderRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Order>()), Times.Never);
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnTrue_WhenOrderIsDeleted()
    {
        // Arrange
        _mockOrderRepository.Setup(repo => repo.DeleteAsync("1")).ReturnsAsync(true);

        // Act
        var result = await _orderService.DeleteAsync("1");

        // Assert
        Assert.True(result);
        _mockOrderRepository.Verify(repo => repo.DeleteAsync("1"), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnFalse_WhenOrderDoesNotExist()
    {
        // Arrange
        _mockOrderRepository.Setup(repo => repo.DeleteAsync("1")).ReturnsAsync(false);

        // Act
        var result = await _orderService.DeleteAsync("1");

        // Assert
        Assert.False(result);
        _mockOrderRepository.Verify(repo => repo.DeleteAsync("1"), Times.Once);
    }
}
