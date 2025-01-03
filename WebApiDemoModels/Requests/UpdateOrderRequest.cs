using System.ComponentModel.DataAnnotations;
using WebApiDemoModels.Enums;

namespace WebApiDemoModels.Requests
{
    public class UpdateOrderRequest
    {
        [Required   ]
        public string Id { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}