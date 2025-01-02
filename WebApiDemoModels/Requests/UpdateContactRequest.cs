using System.ComponentModel.DataAnnotations;

namespace WebApiDemoModels.Requests
{
    public class UpdateContactRequest
    {
        [Required]
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}