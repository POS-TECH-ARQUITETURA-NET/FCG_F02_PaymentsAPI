
using MassTransit;
using Microsoft.OpenApi.Models;
using PaymentsAPI.Consumers;

var builder = WebApplication.CreateBuilder(args);

var rabbitHost = builder.Configuration["RabbitMQ:Host"] ?? "rabbitmq";
var rabbitUser = builder.Configuration["RabbitMQ:User"] ?? "guest";
var rabbitPass = builder.Configuration["RabbitMQ:Pass"] ?? "guest";

builder.Services.AddMassTransit(x => {
    x.AddConsumer<OrdersConsumer>();
    x.UsingRabbitMq((context, cfg) => {
        cfg.Host(rabbitHost, "/", h => { h.Username(rabbitUser); h.Password(rabbitPass); });
        cfg.ReceiveEndpoint("payments-orders", e => {
            e.ConfigureConsumer<OrdersConsumer>(context);
        });
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PaymentsAPI", Version = "v1" });
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();
