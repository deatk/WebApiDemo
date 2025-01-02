using Microsoft.AspNetCore.Mvc;
using WebApiDemoServices.Interfaces;
using WebApiDemoModels;
using WebApiDemoModels.Requests;
using AutoMapper;

namespace WebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrdersAsync()
        {
            var orders = await _orderService.GetAllAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderByIdAsync(string id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null)
                return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderRequest createOrderRequest)
        {
            var createOrder = _mapper.Map<Order>(createOrderRequest);
            var createdOrder = await _orderService.CreateAsync(createOrder);
            return CreatedAtAction(nameof(GetOrderByIdAsync), new { id = createdOrder.Id }, createdOrder);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateOrderAsync(string id, [FromBody] UpdateOrderRequest updateOrderRequest)
        {
            var updateOrder = _mapper.Map<Order>(updateOrderRequest);
            var updatedOrder = await _orderService.UpdateAsync(id, updateOrder);
            if (updatedOrder == null)
                return NotFound();
            return Ok(updatedOrder);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderAsync(string id)
        {
            var deleted = await _orderService.DeleteAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }
}
