using MediatR;

namespace WebApiServiceForVacancy.CQRS.Models.Commands.OrderProductCommands;

public class DeleteOrderProductCommand : IRequest<bool>
{
    public DeleteOrderProductCommand(uint orderId)
    {
        OrderId = orderId;
    }
    public uint OrderId { get; set; }
}