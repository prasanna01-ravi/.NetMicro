
using Discount.Business.data;
using Discount.Business.Extensions;
using Discount.Business.repositories;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddScoped<IDiscountContext, DiscountContext>();
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();

builder.Services.AddGrpc();

var app = builder.Build();
app.MigrateDatabase();
// Configure the HTTP request pipeline.
//app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
