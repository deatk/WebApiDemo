using FluentValidation.AspNetCore;
using Serilog;
using WebApiDemo.Configurations;
using WebApiDemoServices;
using WebApiDemoRepositories;
using WebApiDemoModels.Mappings;
using WebApiDemoModels.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add file connections.json
builder.Configuration.AddJsonFile("connections.json", optional: false, reloadOnChange: true);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddRepositoryServices();
builder.Services.AddAutoMapper(typeof(ContactMappingProfile).Assembly);
builder.Services.AddSerilog();
builder.Services.AddMongoDbServices(builder.Configuration);
builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PizzaRequestValidator>());

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