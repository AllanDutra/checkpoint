using Checkpoint.API.Extensions;
using Checkpoint.Infrastructure;
using Checkpoint.Application;
using Checkpoint.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddValidations();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerExtensions();

builder.Services.AddApplication();

builder.Services.AddDomain();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddNotifications();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = string.Empty;
        c.SwaggerEndpoint("./swagger/v1/swagger.json", "Checkpoint.API");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
