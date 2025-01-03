using WebApiDemo.Configurations;
using WebApiDemoServices;
using WebApiDemoRepositories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add file connections.json
builder.Configuration.AddJsonFile("connections.json", optional: false, reloadOnChange: true);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddRepositoryServices(); // Ensure this matches the method name in ServiceCollectionExtensions.cs
builder.Services.AddAutoMapper();
builder.Services.AddSerilog();
builder.Services.AddMongoDbServices(builder.Configuration); // Ensure this matches the method name in MongoDbExtensions.cs
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