using System.ComponentModel.DataAnnotations;
using WebApiDemoModels.Enums;

namespace WebApiDemoModels.Requests
{
    public class UpdateOrderRequest
    {
        [Required   ]
        public string Id { get; set; }
        
        public string ContactId { get; set; }
        public List<string> OrderDetails { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public decimal Total { get; set; }
    }
}