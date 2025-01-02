using WebApiDemoModels;

namespace WebApiDemoRepositories.Interfaces
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetAllAsync();
        Task<Contact?> GetByIdAsync(string id);
        Task<Contact?> GetByPhoneNumberAsync(string phoneNumber);
        Task<Contact?> GetByEmailAsync(string email);
        Task<Contact> CreateAsync(Contact contact);
        Task<bool> UpdateAsync(string id, Contact contact);
        Task<bool> DeleteAsync(string id);
    }
}
