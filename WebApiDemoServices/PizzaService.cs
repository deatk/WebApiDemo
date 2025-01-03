using WebApiDemoModels;
using WebApiDemoServices.Interfaces;
using WebApiDemoRepositories.Interfaces;

namespace WebApiDemoServices
{
    public class PizzaService : IPizzaService
    {
        private readonly IPizzaRepository _pizzaRepository;

        public PizzaService(IPizzaRepository pizzaRepository)
        {
            _pizzaRepository = pizzaRepository;
        }

        public async Task<IEnumerable<Pizza>> GetAllAsync()
        {
            return await _pizzaRepository.GetAllAsync();
        }

        public async Task<Pizza?> GetByNameAsync(string name)
        {
            return await _pizzaRepository.GetByNameAsync(name);
        }

        public async Task AddAsync(Pizza pizza)
        {
            await _pizzaRepository.AddAsync(pizza);
        }

        public async Task UpdateAsync(Pizza pizza)
        {
            await _pizzaRepository.UpdateAsync(pizza);
        }

        public async Task DeleteAsync(string id)
        {
            await _pizzaRepository.DeleteAsync(id);
        }
    }
}