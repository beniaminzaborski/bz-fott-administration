using Bz.Fott.Administration.Application;
using Bz.Fott.Administration.Infrastructure;
using MediatR;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

// Add services to the container.
services
    .AddApplication()
    .AddInfrastructure(config)
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddControllers();

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
