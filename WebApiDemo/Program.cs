using Serilog;
using WebApiDemoServices.Interfaces;
using WebApiDemoServices;
using WebApiDemoRepositories;

var builder = WebApplication.CreateBuilder(args);

// Add file connections.json
builder.Configuration.AddJsonFile("connections.json", optional: false, reloadOnChange: true);

// Add MongoDB repository
builder.Services.AddMongoDbRepository(builder.Configuration);

// Set up Serilog with Console and File sinks
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()  // Log to the console
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day) // Log to a file with daily rolling
    .CreateLogger();

// Add Serilog as the logging provider
builder.Host.UseSerilog(); // Ensure you have added this using Serilog.AspNetCore package

// Add services to the container.
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddControllers();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();  // This adds the Swagger generator

var app = builder.Build();

// Enable Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  // This generates the Swagger JSON
    app.UseSwaggerUI();  // This enables the Swagger UI
}

app.MapControllers();

app.Run();
