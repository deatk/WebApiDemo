using WebApiDemoModels;

namespace WebApiDemoServices.Interfaces
{
    public interface IPizzaService
    {
        Task<IEnumerable<Pizza>> GetAllAsync();
        Task<Pizza?> GetByNameAsync(string name);
        Task AddAsync(Pizza pizza);
        Task UpdateAsync(Pizza pizza);
        Task DeleteAsync(string id);
    }
}