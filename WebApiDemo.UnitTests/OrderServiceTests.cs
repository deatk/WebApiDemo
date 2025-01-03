using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using WebApiDemoModels;
using WebApiDemoModels.Enums;
using WebApiDemoRepositories.Interfaces;
using WebApiDemoServices;
using Xunit;

public class OrderServiceTests
{
    private readonly Mock<IOrderRepository> _mockOrderRepository;
    private readonly OrderService _orderService;

    public OrderServiceTests()
    {
        _mockOrderRepository = new Mock<IOrderRepository>();
        _orderService = new OrderService(_mockOrderRepository.Object);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllOrders()
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
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsOrder_WhenOrderExists()
    {
        // Arrange
        var order = new Order { Id = "1", ContactId = "C1", Total = 100 };
        _mockOrderRepository.Setup(repo => repo.GetByIdAsync("1")).ReturnsAsync(order);

        // Act
        var result = await _orderService.GetByIdAsync("1");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(order, result);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenOrderDoesNotExist()
    {
        // Arrange
        _mockOrderRepository.Setup(repo => repo.GetByIdAsync("1")).ReturnsAsync((Order)null);

        // Act
        var result = await _orderService.GetByIdAsync("1");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_CreatesOrderWithNewIdAndDate()
    {
        // Arrange
        var order = new Order
        {
            ContactId = "C1",
            OrderDetails = new List<OrderDetail>(),
            Status = OrderStatus.Pending
        };
        _mockOrderRepository.Setup(repo => repo.CreateAsync(It.IsAny<Order>())).ReturnsAsync((Order o) => o);

        // Act
        var result = await _orderService.CreateAsync(order);

        // Assert
        Assert.NotNull(result.Id);
        Assert.NotEqual(default, result.OrderDate);
        _mockOrderRepository.Verify(repo => repo.CreateAsync(order), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_UpdatesOrder_WhenOrderExists()
    {
        // Arrange
        var existingOrder = new Order { Id = "1", ContactId = "C1", Total = 100 };
        var updatedOrder = new Order { Id = "1", ContactId = "C1", Total = 150 };
        _mockOrderRepository.Setup(repo => repo.GetByIdAsync("1")).ReturnsAsync(existingOrder);
        _mockOrderRepository.Setup(repo => repo.UpdateAsync(updatedOrder)).ReturnsAsync(updatedOrder);

        // Act
        var result = await _orderService.UpdateAsync("1", updatedOrder);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(updatedOrder.Total, result.Total);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsNull_WhenOrderDoesNotExist()
    {
        // Arrange
        var updatedOrder = new Order { Id = "1", ContactId = "C1", Total = 150 };
        _mockOrderRepository.Setup(repo => repo.GetByIdAsync("1")).ReturnsAsync((Order)null);

        // Act
        var result = await _orderService.UpdateAsync("1", updatedOrder);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsTrue_WhenOrderIsDeleted()
    {
        // Arrange
        _mockOrderRepository.Setup(repo => repo.DeleteAsync("1")).ReturnsAsync(true);

        // Act
        var result = await _orderService.DeleteAsync("1");

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsFalse_WhenOrderDoesNotExist()
    {
        // Arrange
        _mockOrderRepository.Setup(repo => repo.DeleteAsync("1")).ReturnsAsync(false);

        // Act
        var result = await _orderService.DeleteAsync("1");

        // Assert
        Assert.False(result);
    }
}
