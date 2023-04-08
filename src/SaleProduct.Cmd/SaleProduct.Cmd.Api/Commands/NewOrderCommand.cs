using Sdk.Cqrs.Commands;

namespace SaleProduct.Cmd.Api.Commands;

public class NewOrderCommand : BaseCommand
{
    public string Vendor { get; set; }
    public string Description { get; set; }
}
