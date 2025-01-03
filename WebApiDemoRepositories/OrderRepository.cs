using MongoDB.Driver;
using WebApiDemoModels;
using WebApiDemoRepositories.Interfaces;

namespace WebApiDemoRepositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<Order> _orders;

        public OrderRepository(IMongoDatabase database)
        {
            _orders = database.GetCollection<Order>("Orders");
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _orders.Find(_ => true).ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(string id)
        {
            return await _orders.Find(o => o.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Order> CreateAsync(Order order)
        {
            if (string.IsNullOrWhiteSpace(order.Id))
            {
                order.Id = Guid.NewGuid().ToString(); // Generate a unique ID if not provided
            }

            if (order.OrderDate == default)
            {
                order.OrderDate = DateTime.UtcNow; // Set the order time to now if not provided
            }

            await _orders.InsertOneAsync(order);
            return order;
        }

        public async Task<Order?> UpdateAsync(Order order)
        {
            var result = await _orders.ReplaceOneAsync(o => o.Id == order.Id, order);
            return result.IsAcknowledged && result.ModifiedCount > 0 ? order : null;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _orders.DeleteOneAsync(o => o.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
