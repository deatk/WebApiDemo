using Microsoft.AspNetCore.Mvc;
using WebApiDemoServices.Interfaces;
using WebApiDemoModels;
using WebApiDemoModels.Requests;
using AutoMapper;

namespace WebApiDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;

        public ContactController(IContactService contactService, IMapper mapper)
        {
            _contactService = contactService;
            _mapper = mapper;
        }

        // GET: api/contact
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var contacts = await _contactService.GetAllAsync();
            return Ok(contacts);
        }

        // GET: api/contact/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var contact = await _contactService.GetByIdAsync(id);

            if (contact == null)
                return NotFound();

            return Ok(contact);
        }

        // GET: api/contact/by-phone/{phoneNumber}
        [HttpGet("by-phone/{phoneNumber}")]
        public async Task<IActionResult> GetByPhoneNumber(string phoneNumber)
        {
            var contact = await _contactService.GetByPhoneNumberAsync(phoneNumber);

            if (contact == null)
                return NotFound();

            return Ok(contact);
        }

        // GET: api/contact/by-email/{email}
        [HttpGet("by-email/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var contact = await _contactService.GetByEmailAsync(email);

            if (contact == null)
                return NotFound();

            return Ok(contact);
        }

        // POST: api/contact
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateContactRequest createContactRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var createContact = _mapper.Map<Contact>(createContactRequest);
            var createdContact = await _contactService.CreateAsync(createContact);

            return CreatedAtAction(nameof(GetById), new { id = createdContact.Id }, createdContact);
        }

        // PUT: api/contact/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateContactRequest updateContactRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updateContact = _mapper.Map<Contact>(updateContactRequest);
            var updated = await _contactService.UpdateAsync(id, updateContact);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/contact/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var deleted = await _contactService.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
