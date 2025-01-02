using WebApiDemoModels;

namespace WebApiDemoServices.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order?> GetByIdAsync(string id);
        Task<Order> CreateAsync(Order order);
        Task<Order?> UpdateAsync(string id, Order order);
        Task<bool> DeleteAsync(string id);
    }
}
