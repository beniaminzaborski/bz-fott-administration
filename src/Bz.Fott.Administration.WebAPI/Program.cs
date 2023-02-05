using Bz.Fott.Administration.Application;
using Bz.Fott.Administration.Infrastructure;
using Bz.Fott.Administration.WebAPI;
using Microsoft.Extensions.Configuration;

const string serviceName = "Fott-Administration";
const string serviceVersion = "1.0.0";

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

// Add services to the container.
services
    .AddTelemetry(serviceName, serviceVersion)
    .AddApplication()
    .AddInfrastructure(config)
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddCustomControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger()
       .UseSwaggerUI();
}

app.UseHttpsRedirection()
   .UseAuthorization();

app.MapControllers();

app.MigrateDb();

app.Run();
