using System.ComponentModel.DataAnnotations;
using WebApiDemoModels.Enums;

namespace WebApiDemoModels.Requests
{
    public class UpdatePizzaRequest
    {
        [Required]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<string> Ingredients { get; set; }
    }
}