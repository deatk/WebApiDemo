using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using WebApiDemoModels;
using WebApiDemoRepositories.Interfaces;
using WebApiDemoServices;
using Xunit;

public class ContactServiceTests
{
    private readonly Mock<IContactRepository> _mockRepository;
    private readonly ContactService _contactService;

    public ContactServiceTests()
    {
        _mockRepository = new Mock<IContactRepository>();
        _contactService = new ContactService(_mockRepository.Object);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllContacts()
    {
        // Arrange
        var expectedContacts = new List<Contact>
        {
            new Contact { Id = "1", Name = "John Doe", Email = "john@example.com" },
            new Contact { Id = "2", Name = "Jane Doe", Email = "jane@example.com" }
        };
        _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(expectedContacts);

        // Act
        var result = await _contactService.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedContacts.Count, result.Count());
    }

    [Fact]
    public async Task GetByIdAsync_ExistingId_ReturnsContact()
    {
        // Arrange
        var expectedContact = new Contact { Id = "1", Name = "John Doe" };
        _mockRepository.Setup(repo => repo.GetByIdAsync("1")).ReturnsAsync(expectedContact);

        // Act
        var result = await _contactService.GetByIdAsync("1");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedContact.Id, result.Id);
        Assert.Equal(expectedContact.Name, result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingId_ReturnsNull()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.GetByIdAsync("2")).ReturnsAsync((Contact)null);

        // Act
        var result = await _contactService.GetByIdAsync("2");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetByPhoneNumberAsync_ExistingPhoneNumber_ReturnsContact()
    {
        // Arrange
        var contacts = new List<Contact>
        {
            new Contact { Id = "1", Name = "John Doe", PhoneNumber = "1234567890" }
        };
        _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(contacts);

        // Act
        var result = await _contactService.GetByPhoneNumberAsync("1234567890");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("1234567890", result.PhoneNumber);
    }

    [Fact]
    public async Task GetByPhoneNumberAsync_NonExistingPhoneNumber_ReturnsNull()
    {
        // Arrange
        var contacts = new List<Contact>();
        _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(contacts);

        // Act
        var result = await _contactService.GetByPhoneNumberAsync("9999999999");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_ValidContact_ReturnsCreatedContact()
    {
        // Arrange
        var contactToCreate = new Contact { Id = "1", Name = "John Doe" };
        _mockRepository.Setup(repo => repo.CreateAsync(contactToCreate)).ReturnsAsync(contactToCreate);

        // Act
        var result = await _contactService.CreateAsync(contactToCreate);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(contactToCreate.Id, result.Id);
    }

    [Fact]
    public async Task UpdateAsync_ExistingId_ReturnsTrue()
    {
        // Arrange
        var existingContact = new Contact { Id = "1", Name = "John Doe" };
        _mockRepository.Setup(repo => repo.GetByIdAsync("1")).ReturnsAsync(existingContact);
        _mockRepository.Setup(repo => repo.UpdateAsync("1", existingContact)).ReturnsAsync(true);

        // Act
        var result = await _contactService.UpdateAsync("1", existingContact);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task UpdateAsync_NonExistingId_ReturnsFalse()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.GetByIdAsync("2")).ReturnsAsync((Contact)null);

        // Act
        var result = await _contactService.UpdateAsync("2", new Contact());

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task DeleteAsync_ExistingId_ReturnsTrue()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.DeleteAsync("1")).ReturnsAsync(true);

        // Act
        var result = await _contactService.DeleteAsync("1");

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAsync_NonExistingId_ReturnsFalse()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.DeleteAsync("2")).ReturnsAsync(false);

        // Act
        var result = await _contactService.DeleteAsync("2");

        // Assert
        Assert.False(result);
    }
}
