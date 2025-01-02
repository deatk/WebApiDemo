using System.ComponentModel.DataAnnotations;

namespace WebApiDemoModels.Requests
{
    public class CreateContactRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}