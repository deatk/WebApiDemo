using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; // Importa il namespace per il logging
using WebApiDemoServices.Interfaces; // Importa l'interfaccia dal progetto dei servizi

namespace WebApiDemo.Controllers
{
    [ApiController]
    [Route("Contact")]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly ILogger<ContactController> _logger; // Aggiungi il logger

        // Modifica il costruttore per accettare ILogger<ContactController>
        public ContactController(IContactService contactService, ILogger<ContactController> logger)
        {
            _contactService = contactService;
            _logger = logger; // Inizializza il logger
        }

        [HttpGet("GetContact")]
        public async Task<IActionResult> GetContact()
        {
            _logger.LogInformation("Getting contact..."); // Usa il logger per registrare informazioni

            var contact = await _contactService.GetContact();

            if (contact == null)
            {
                _logger.LogWarning("Contact not found");
                return NotFound();
            }

            _logger.LogInformation("Contact retrieved successfully");
            return Ok(contact);
        }
    }
}
