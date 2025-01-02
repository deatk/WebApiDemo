using WebApiDemoModels;

namespace WebApiDemoServices.Interfaces
{
    public interface IContactService
    {
        /// <summary>
        /// Retrieves all contacts.
        /// </summary>
        /// <returns>A list of contacts.</returns>
        Task<IEnumerable<Contact>> GetAllAsync();

        /// <summary>
        /// Retrieves a contact by its ID.
        /// </summary>
        /// <param name="id">The contact ID.</param>
        /// <returns>The contact or null if not found.</returns>
        Task<Contact> GetByIdAsync(string id);

        /// <summary>
        /// Retrieves a contact by phone number.
        /// </summary>
        /// <param name="phoneNumber">The phone number.</param>
        /// <returns>The contact DTO or null if not found.</returns>
        Task<Contact> GetByPhoneNumberAsync(string phoneNumber);

        /// <summary>
        /// Retrieves a contact by email.
        /// </summary>
        /// <param name="email">The email address.</param>
        /// <returns>The contact or null if not found.</returns>
        Task<Contact> GetByEmailAsync(string email);

        /// <summary>
        /// Creates a new contact.
        /// </summary>
        /// <param name="contact">The contact data to create.</param>
        /// <returns>The created contact DTO.</returns>
        Task<Contact> CreateAsync(Contact contact);

        /// <summary>
        /// Updates an existing contact.
        /// </summary>
        /// <param name="id">The ID of the contact to update.</param>
        /// <param name="contact">The updated contact data.</param>
        /// <returns>True if the update was successful, otherwise false.</returns>
        Task<bool> UpdateAsync(string id, Contact contact);

        /// <summary>
        /// Deletes a contact by its ID.
        /// </summary>
        /// <param name="id">The ID of the contact to delete.</param>
        /// <returns>True if the contact was successfully deleted, otherwise false.</returns>
        Task<bool> DeleteAsync(string id);
    }
}
