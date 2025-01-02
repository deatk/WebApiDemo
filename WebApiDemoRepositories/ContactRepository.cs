using MongoDB.Driver;
using WebApiDemoModels;
using WebApiDemoRepositories.Interfaces;

namespace WebApiDemoRepositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly IMongoCollection<Contact> _contacts;

        public ContactRepository(IMongoDatabase database)
        {
            _contacts = database.GetCollection<Contact>("Contacts");
        }

        public async Task<List<Contact>> GetAllAsync()
        {
            return await _contacts.Find(_ => true).ToListAsync();
        }

        public async Task<Contact?> GetByIdAsync(string id)
        {
            return await _contacts.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Contact?> GetByPhoneNumberAsync(string phoneNumber)
        {
            return await _contacts.Find(c => c.PhoneNumber == phoneNumber).FirstOrDefaultAsync();
        }

        public async Task<Contact?> GetByEmailAsync(string email)
        {
            return await _contacts.Find(c => c.Email == email).FirstOrDefaultAsync();
        }

        public async Task<Contact> CreateAsync(Contact contact)
        {
            await _contacts.InsertOneAsync(contact);
            return contact;
        }

        public async Task<bool> UpdateAsync(string id, Contact contact)
        {
            var result = await _contacts.ReplaceOneAsync(c => c.Id == id, contact);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _contacts.DeleteOneAsync(c => c.Id == id);
            return result.DeletedCount > 0;
        }
    }
}
