using System.ComponentModel.DataAnnotations;
using WebApiDemoModels.Enums;

namespace WebApiDemoModels.Requests
{
    public class CreateOrderRequest
    {
        [Required]
        public string ContactId { get; set; }
        [Required]
        public List<string> OrderDetails { get; set; }
        public decimal Total { get; set; }
    }
}