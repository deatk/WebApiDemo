using AutoMapper;
using Serilog;
using WebApiDemoServices.Interfaces;
using WebApiDemoServices;
using WebApiDemoRepositories;
using WebApiDemoModels;

var builder = WebApplication.CreateBuilder(args);

// Add file connections.json
builder.Configuration.AddJsonFile("connections.json", optional: false, reloadOnChange: true);

// Add AutoMapper with profiles
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Add MongoDB repository
builder.Services.AddMongoDbRepository(builder.Configuration);

// Set up Serilog with Console and File sinks
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()  
    .WriteTo.File(Path.Combine(AppContext.BaseDirectory, "logs", "log.txt"), rollingInterval: RollingInterval.Day)
    .CreateLogger();

// Add Serilog as the logging provider
builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddControllers();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiDemo v1"));
}

app.MapControllers();

app.Run();
