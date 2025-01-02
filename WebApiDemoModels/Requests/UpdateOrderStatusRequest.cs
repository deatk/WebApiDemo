using WebApiDemoModels.Enums;
using System.ComponentModel.DataAnnotations;

namespace WebApiDemoModels.Requests
{
    public class UpdateOrderStatusRequest
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public OrderStatus Status { get; set; }
    }
}