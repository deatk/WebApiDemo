using WebApiDemoModels;
using WebApiDemoRepositories.Interfaces;
using WebApiDemoServices.Interfaces;

namespace WebApiDemoServices
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
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
            return await _orderRepository.CreateAsync(order);
        }

        public async Task<Order?> UpdateAsync(string id, Order order)
        {
            var existingOrder = await _orderRepository.GetByIdAsync(id);
            if (existingOrder == null)
                return null;

            order.Id = id; // Ensure the same ID is kept
            return await _orderRepository.UpdateAsync(order);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await _orderRepository.DeleteAsync(id);
        }
    }
}
