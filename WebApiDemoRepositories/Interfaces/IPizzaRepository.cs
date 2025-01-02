using WebApiDemoModels;

namespace WebApiDemoRepositories.Interfaces
{
    public interface IPizzaRepository
    {
        Task<IEnumerable<Pizza>> GetAllAsync();
        Task<Pizza?> GetByIdAsync(string id);
        Task AddAsync(Pizza pizza);
        Task UpdateAsync(Pizza pizza);
        Task DeleteAsync(string id);
    }
}