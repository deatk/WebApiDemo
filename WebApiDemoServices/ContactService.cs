using WebApiDemoServices.Interfaces;

namespace WebApiDemoServices
{
    public class ContactService : IContactService
    {
        public async Task<string> GetContact()
        {
            // Metodo fittizio che simula il recupero di un contatto
            await Task.Delay(100); // Simula un'operazione asincrona
            return "John Doe";
        }
    }
}
