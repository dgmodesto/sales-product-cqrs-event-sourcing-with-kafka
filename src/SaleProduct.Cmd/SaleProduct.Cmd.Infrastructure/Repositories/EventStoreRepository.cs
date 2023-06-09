﻿using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SaleProduct.Cmd.Infrastructure.Config;
using Sdk.Cqrs.Domain;
using Sdk.Cqrs.Events;

namespace SaleProduct.Cmd.Infrastructure.Repositories;

public class EventStoreRepository : IEventStoreRepository
{
    private readonly IMongoCollection<EventModel> _eventStoreCollection;

    public EventStoreRepository(IOptions<MongoDbConfig> config)
    {
        var mongoClient = new MongoClient(config.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(config.Value.Database);

        _eventStoreCollection = mongoDatabase.GetCollection<EventModel>(config.Value.Collection);
    }

    public async Task<List<EventModel>> FindAllAsync()
    {
        return await _eventStoreCollection.Find(_ => true).ToListAsync().ConfigureAwait(false);
    }

    public async Task<List<EventModel>> FindByAggregateId(Guid aggregateId)
    {
        return await _eventStoreCollection.Find(x => x.AggregateIndentifier == aggregateId).ToListAsync().ConfigureAwait(false);
    }

    public async Task SaveAsync(EventModel @event)
    {
        await _eventStoreCollection.InsertOneAsync(@event).ConfigureAwait(false);
    }
}
