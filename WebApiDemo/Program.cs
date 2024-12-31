using WebApiDemoServices;
using WebApiDemoServices.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Aggiungi i servizi al container
builder.Services.AddScoped<IContactService, ContactService>();

var app = builder.Build();

// Configura il middleware
app.MapControllers();

app.Run();