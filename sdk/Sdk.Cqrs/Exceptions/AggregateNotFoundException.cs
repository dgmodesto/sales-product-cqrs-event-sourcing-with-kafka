namespace Sdk.Cqrs.Exceptions;

public class AggregateNotFoundException : Exception
{
    public AggregateNotFoundException(string messsage) : base(messsage)
    {
        
    }
}
