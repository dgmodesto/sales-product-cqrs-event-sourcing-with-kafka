﻿namespace Sdk.Cqrs.Events;

public class EventModel
{
    public Guid Id { get; set; }
    public DateTime TimeStamp { get; set; }
    public Guid AggregateIndentifier { get; set; }
    public string AggregateType { get; set; }
    public int Version { get; set; }
    public string EventType { get; set; }
    public BaseEvent EventData { get; set; }
}
