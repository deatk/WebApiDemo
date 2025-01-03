using WebApiDemoModels.Enums;

namespace WebApiDemoModels
{
    public class Pizza
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<Toppings> Ingredients { get; set; }
    }
}
