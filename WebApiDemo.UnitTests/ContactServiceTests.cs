using Moq;
using Xunit;
using WebApiDemoServices;
using WebApiDemoServices.Interfaces;
using WebApiDemoRepositories.Interfaces;
using WebApiDemoModels;

public class ContactServiceTests
{
    private readonly Mock<IContactRepository> _mockRepository;
    private readonly IContactService _contactService;

    public ContactServiceTests()
    {
        _mockRepository = new Mock<IContactRepository>();
        _contactService = new ContactService(_mockRepository.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnContacts()
    {
        // Arrange
        var contacts = new List<Contact> { new Contact { Id = "1", Name = "John Doe" } };
        _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(contacts);

        // Act
        var result = await _contactService.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
    }
}
