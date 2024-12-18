using DineConnect.OrderManagementService.API;
using DineConnect.OrderManagementService.Application;
using DineConnect.OrderManagementService.Infrastructure;
using Infrastructure.IntegrationEvents;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddServiceBlockInfrastructure("DefaultConnection")
    .AddOrderManagementInfrastructureDependencies(builder.Configuration)
    .AddOrderManagementContractsDependencies(builder.Configuration)
    .AddOrderManagementApplicationDependencies(builder.Configuration);
    
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseExceptionHandler(_ => { });
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
IntializeStartup.Start(app);

app.Run();

IntializeStartup.Shutdown(app);

