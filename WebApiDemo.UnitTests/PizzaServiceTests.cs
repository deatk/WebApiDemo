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

namespace WebApiDemoTests
{
    public class PizzaServiceTests
    {
        private readonly Mock<IPizzaRepository> _pizzaRepositoryMock;
        private readonly PizzaService _pizzaService;

        public PizzaServiceTests()
        {
            _pizzaRepositoryMock = new Mock<IPizzaRepository>();
            _pizzaService = new PizzaService(_pizzaRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllPizzas()
        {
            // Arrange
            var pizzas = new List<Pizza>
            {
                new Pizza { Name = "Margherita", Description = "Classic", Price = 7.99m, Ingredients = new List<Toppings> { Toppings.Mozzarella, Toppings.TomatoSauce } },
                new Pizza { Name = "Pepperoni", Description = "Spicy", Price = 9.99m, Ingredients = new List<Toppings> { Toppings.Mozzarella, Toppings.Pepperoni } }
            };
            _pizzaRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(pizzas);

            // Act
            var result = await _pizzaService.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, p => p.Name == "Margherita");
            Assert.Contains(result, p => p.Name == "Pepperoni");
        }

        [Fact]
        public async Task GetByNameAsync_ShouldReturnPizza_WhenPizzaExists()
        {
            // Arrange
            var pizza = new Pizza { Name = "Margherita", Description = "Classic", Price = 7.99m, Ingredients = new List<Toppings> { Toppings.Mozzarella, Toppings.TomatoSauce } };
            _pizzaRepositoryMock.Setup(repo => repo.GetByNameAsync("Margherita")).ReturnsAsync(pizza);

            // Act
            var result = await _pizzaService.GetByNameAsync("Margherita");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Margherita", result.Name);
        }

        [Fact]
        public async Task GetByNameAsync_ShouldReturnNull_WhenPizzaDoesNotExist()
        {
            // Arrange
            _pizzaRepositoryMock.Setup(repo => repo.GetByNameAsync("NonExistent")).ReturnsAsync((Pizza)null);

            // Act
            var result = await _pizzaService.GetByNameAsync("NonExistent");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddAsync_ShouldAddPizza()
        {
            // Arrange
            var pizza = new Pizza { Name = "Margherita", Description = "Classic", Price = 7.99m, Ingredients = new List<Toppings> { Toppings.Mozzarella, Toppings.TomatoSauce } };

            // Act
            await _pizzaService.AddAsync(pizza);

            // Assert
            _pizzaRepositoryMock.Verify(repo => repo.AddAsync(pizza), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdatePizza()
        {
            // Arrange
            var pizza = new Pizza { Name = "Margherita", Description = "Updated Classic", Price = 8.99m, Ingredients = new List<Toppings> { Toppings.Mozzarella, Toppings.TomatoSauce, Toppings.Olives } };

            // Act
            await _pizzaService.UpdateAsync(pizza);

            // Assert
            _pizzaRepositoryMock.Verify(repo => repo.UpdateAsync(pizza), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeletePizza()
        {
            // Arrange
            var pizzaId = "12345";

            // Act
            await _pizzaService.DeleteAsync(pizzaId);

            // Assert
            _pizzaRepositoryMock.Verify(repo => repo.DeleteAsync(pizzaId), Times.Once);
        }
    }
}
