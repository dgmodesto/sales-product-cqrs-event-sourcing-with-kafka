using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using SaleProduct.Query.Api.Queries;
using SaleProduct.Query.Domain.Entities;
using SaleProduct.Query.Domain.Repositories;
using SaleProduct.Query.Infrastructure.Consumers;
using SaleProduct.Query.Infrastructure.DataAccess;
using SaleProduct.Query.Infrastructure.Dispatchers;
using SaleProduct.Query.Infrastructure.Handlers;
using SaleProduct.Query.Infrastructure.Repositories;
using Sdk.Cqrs.Consumers;
using Sdk.Cqrs.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Action<DbContextOptionsBuilder> configurationDbContext = (o => o.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
builder.Services.AddDbContext<DatabaseContext>(configurationDbContext);
builder.Services.AddSingleton<DatabaseContextFactory>(new DatabaseContextFactory(configurationDbContext));

//Create database and tables from code 
var databaseContext = builder.Services.BuildServiceProvider().GetRequiredService<DatabaseContext>();
databaseContext.Database.EnsureCreated();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IQueryHandler, QueryHandler>();

builder.Services.AddScoped<IEventHandler, SaleProduct.Query.Infrastructure.Handlers.EventHandler>();
builder.Services.Configure<ConsumerConfig>(builder.Configuration.GetSection(nameof(ConsumerConfig)));
builder.Services.AddScoped<IEventConsumer, EventConsumer>();

//Register Query Handlers Methods 
var queryHandler = builder.Services.BuildServiceProvider().GetRequiredService<IQueryHandler>();
var dispatcher = new QueryDispatcher();
dispatcher.RegisterHandler<FindAllOrderQuery>(queryHandler.HandleAsync);
builder.Services.AddSingleton<IQueryDispatcher<OrderEntity>>(_ => dispatcher);

builder.Services.AddControllers();
builder.Services.AddHostedService<ConsumerHostedService>();
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
