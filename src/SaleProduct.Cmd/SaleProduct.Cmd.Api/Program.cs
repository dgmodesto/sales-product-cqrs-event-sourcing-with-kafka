using SaleProduct.Cmd.Domain.Aggregates;
using SaleProduct.Cmd.Infrastructure.Handlers;
using SaleProduct.Cmd.Infrastructure.Stores;
using Sdk.Cqrs.Domain;
using Sdk.Cqrs.Hanlders;
using Sdk.Cqrs.Infrastructure;
using Sdk.Cqrs.Producers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IEventSourcingHandler<OrderAggregate>, EventSourcingHandler>();
//builder.Services.AddScoped<ICommandHandler, CommandHandler>();
//builder.Services.AddScoped<IEventProducer, EventProducer>();
builder.Services.AddScoped<IEventStore, EventStore>();
//builder.Services.AddScoped<IEventStoreRepository, EventStoreRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
