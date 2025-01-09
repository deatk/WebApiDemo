using WebApiDemoModels;
using WebApiDemoRepositories.Interfaces;
using WebApiDemoServices.Interfaces;
using WebApiDemoModels.Enums;

namespace WebApiDemoServices
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPizzaRepository _pizzaRepository;

        public OrderService(IOrderRepository orderRepository, IPizzaRepository pizzaRepository)
        {
            _orderRepository = orderRepository;
            _pizzaRepository = pizzaRepository;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<Order?> GetByIdAsync(string id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }

        public async Task<Order> CreateAsync(Order order)
        {
            order.Id = Guid.NewGuid().ToString();
            order.OrderDate = DateTime.UtcNow;
            order.Status = OrderStatus.Pending;
            order.Total = await CalculateTotal(order.OrderDetails);
            return await _orderRepository.CreateAsync(order);
        }

        public async Task<Order?> UpdateAsync(string id, Order order)
        {
            var existingOrder = await _orderRepository.GetByIdAsync(id);
            if (existingOrder == null)
                return null;

            order.Id = id;
            order.Total = await CalculateTotal(order.OrderDetails);
            return await _orderRepository.UpdateAsync(order);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await _orderRepository.DeleteAsync(id);
        }

        private async Task<decimal> CalculateTotal(IEnumerable<OrderDetail> orderDetails)
        {
            decimal total = 0;
            foreach (var orderDetail in orderDetails)
            {
                var pizza = await _pizzaRepository.GetByNameAsync(orderDetail.PizzaName);
                if (pizza == null)
                    throw new Exception($"Pizza with name {orderDetail.PizzaName} not found");
                total += orderDetail.Quantity * pizza.Price;
            }
            return total;
        }
    }
}
