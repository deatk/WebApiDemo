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

            // Ensure unique index on Email
            var emailIndex = Builders<Contact>.IndexKeys.Ascending(c => c.Email);
            _contacts.Indexes.CreateOne(new CreateIndexModel<Contact>(emailIndex, new CreateIndexOptions { Unique = true }));

            // Ensure unique index on PhoneNumber
            var phoneIndex = Builders<Contact>.IndexKeys.Ascending(c => c.PhoneNumber);
            _contacts.Indexes.CreateOne(new CreateIndexModel<Contact>(phoneIndex, new CreateIndexOptions { Unique = true }));
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
            if (string.IsNullOrWhiteSpace(contact.Id))
            {
                contact.Id = Guid.NewGuid().ToString(); // Generate a unique ID if not provided
            }

            try
            {
                await _contacts.InsertOneAsync(contact);
                return contact;
            }
            catch (MongoWriteException ex) when (ex.WriteError.Category == ServerErrorCategory.DuplicateKey)
            {
                throw new InvalidOperationException("A contact with the same email or phone number already exists.", ex);
            }
        }

        public async Task<bool> UpdateAsync(string id, Contact contact)
        {
            try
            {
                var result = await _contacts.ReplaceOneAsync(c => c.Id == id, contact);
                return result.ModifiedCount > 0;
            }
            catch (MongoWriteException ex) when (ex.WriteError.Category == ServerErrorCategory.DuplicateKey)
            {
                throw new InvalidOperationException("A contact with the same email or phone number already exists.", ex);
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _contacts.DeleteOneAsync(c => c.Id == id);
            return result.DeletedCount > 0;
        }
    }
}
