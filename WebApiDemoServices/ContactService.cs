using WebApiDemoModels;
using WebApiDemoRepositories.Interfaces;
using WebApiDemoServices.Interfaces;

public class ContactService : IContactService
{
    private readonly IContactRepository _contactRepository;

    public ContactService(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    public async Task<IEnumerable<Contact>> GetAllAsync()
    {
        var contacts = await _contactRepository.GetAllAsync();
        return contacts;
    }

    public async Task<Contact> GetByIdAsync(string id)
    {
        var contact = await _contactRepository.GetByIdAsync(id);
        return contact;
    }

    public async Task<Contact?> GetByPhoneNumberAsync(string phoneNumber)
    {
        var contacts = await _contactRepository.GetAllAsync();
        var contact = contacts.FirstOrDefault(c => c.PhoneNumber == phoneNumber);
        return contact;
    }

    public async Task<Contact> GetByEmailAsync(string email)
    {
        var contacts = await _contactRepository.GetAllAsync();
        var contact = contacts.FirstOrDefault(c => c.Email == email);
        return contact;
    }

    public async Task<Contact> CreateAsync(Contact contact)
    {
        var createdContact = await _contactRepository.CreateAsync(contact);
        return createdContact;
    }

    public async Task<bool> UpdateAsync(string id, Contact contact)
    {
        var existingContact = await _contactRepository.GetByIdAsync(id);
        if (existingContact == null)
        {
            return false;
        }

        return await _contactRepository.UpdateAsync(id, contact);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        return await _contactRepository.DeleteAsync(id);
    }
}
