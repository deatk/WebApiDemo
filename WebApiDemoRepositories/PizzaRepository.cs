using MongoDB.Driver;
using WebApiDemoModels;
using WebApiDemoRepositories.Interfaces;

namespace WebApiDemoRepositories
{
    public class PizzaRepository : IPizzaRepository
    {
        private readonly IMongoCollection<Pizza> _collection;

        public PizzaRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<Pizza>("Pizzas");
        }

        public async Task<IEnumerable<Pizza>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<Pizza?> GetByIdAsync(string id)
        {
            return await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddAsync(Pizza pizza)
        {
            await _collection.InsertOneAsync(pizza);
        }

        public async Task UpdateAsync(Pizza pizza)
        {
            await _collection.ReplaceOneAsync(p => p.Id == pizza.Id, pizza);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(p => p.Id == id);
        }
    }
}