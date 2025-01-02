namespace WebApiDemoModels
{
    public class Pizza
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Toppings { get; set; }
        public decimal Price { get; set; }
    }
}
