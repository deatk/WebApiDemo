using System.ComponentModel.DataAnnotations;
using WebApiDemoModels.Enums;

namespace WebApiDemoModels.Requests
{
    public class UpdatePizzaRequest
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<Toppings> Ingredients { get; set; }
    }
}