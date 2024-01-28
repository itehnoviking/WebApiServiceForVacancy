using MediatR;

namespace WebApiServiceForVacancy.CQRS.Models.Commands.OrderProductCommands;

public class SetOrderProductCommand : IRequest<bool>
{
    public SetOrderProductCommand(uint orderId, IEnumerable<uint> productIds)
    {
        OrderId = orderId;
        ProductIds = productIds;
    }

    public uint OrderId { get; set; }
    public IEnumerable<uint> ProductIds { get; set; }
}