using System.ComponentModel.DataAnnotations;
using WebApiDemoModels.Enums;

namespace WebApiDemoModels.Requests
{
    public class CreatePizzaRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public List<Toppings> Ingredients { get; set; }
    }
}