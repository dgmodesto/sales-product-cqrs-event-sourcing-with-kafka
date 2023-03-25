using Sdk.Cqrs.Messages;

namespace Sdk.Cqrs.Events;

public abstract class BaseEvent : Message
{
    public int Version { get; set; }
    public string Type { get; set; }

    protected BaseEvent(string type)
    {
        Type = type;
    }
}
