using Sdk.Cqrs.Events;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sdk.Cqrs.Domain;

public abstract class AggregateRoot
{
    protected Guid _id;
    private readonly List<BaseEvent> _changes = new();

    public Guid Id
    {
        get { return _id; }
    }

    public int Version { get; set; }

    public IEnumerable<BaseEvent> GetUnCommittedChanges()
    {
        return _changes;
    }

    public void MarkchangeAsCommitted()
    {
        _changes.Clear();
    }

    private void ApplyChange(BaseEvent @event, bool isNew)
    {
        var method = this.GetType().GetMethod("Apply", new Type[] { @event.GetType() });

        if(method is not { })
        {
            throw new ArgumentNullException(nameof(method), $"The Apply method was not found in the aggregate for { @event.GetType().Name }");
        }

        method.Invoke(this, new object[] { @event });
        if(isNew)
        {
            _changes.Add(@event);
        }
    }

    protected void RaiseEvent(BaseEvent @event)
    {
        ApplyChange(@event, true);
    }

    public void ReplayEvents(IEnumerable<BaseEvent> events)
    {
        foreach(BaseEvent @event in events)
        {
            ApplyChange(@event, false);
        }
    }
}
