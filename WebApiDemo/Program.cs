using Microsoft.AspNetCore.Mvc;
using WebApiDemoServices.Interfaces;
using WebApiDemoServices;

var builder = WebApplication.CreateBuilder(args);

// Aggiungi i servizi al container DI
builder.Services.AddScoped<IContactService, ContactService>(); // Registriamo il servizio IContactService
builder.Services.AddControllers(); // Aggiungi il supporto per i controller

var app = builder.Build();

// Mappa i controller
app.MapControllers(); // Qui mappi i controller

app.Run();
