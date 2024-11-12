using DineConnect.PromotionsManagementService.API.Services;
using DineConnect.PromotionsManagementService.Application;
using DineConnect.PromotionsManagementService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddPromotionsManagementInfrastructureDependencies(builder.Configuration)
    .AddPromotionsManagementApplicationDependencies(builder.Configuration);

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<FlashSaleServiceController>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
