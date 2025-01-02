using Microsoft.AspNetCore.Mvc;
using WebApiDemoServices.Interfaces;
using WebApiDemoModels;
using WebApiDemoModels.Requests;
using AutoMapper;

namespace WebApiDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PizzaController : ControllerBase
    {
        private readonly IPizzaService _pizzaService;
        private readonly IMapper _mapper;

        public PizzaController(IPizzaService pizzaService, IMapper mapper)
        {
            _pizzaService = pizzaService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pizzas = await _pizzaService.GetAllAsync();
            return Ok(pizzas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var pizza = await _pizzaService.GetByIdAsync(id);
            if (pizza == null)
            {
                return NotFound();
            }
            return Ok(pizza);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePizzaRequest createPizzaRequest)
        {
            var pizza = _mapper.Map<Pizza>(createPizzaRequest);
            await _pizzaService.AddAsync(pizza);
            return CreatedAtAction(nameof(GetAll), null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdatePizzaRequest updatePizzaRequest)
        {
            var pizza = _mapper.Map<Pizza>(updatePizzaRequest);
            await _pizzaService.UpdateAsync(pizza);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _pizzaService.DeleteAsync(id);
            return NoContent();
        }
    }
}