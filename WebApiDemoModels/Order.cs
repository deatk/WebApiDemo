using WebApiDemoModels.Enums;

namespace WebApiDemoModels
{
    public class Order
    {
        public string Id { get; set; }
        
        public string ContactId { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public decimal Total { get; set; }
    }
}