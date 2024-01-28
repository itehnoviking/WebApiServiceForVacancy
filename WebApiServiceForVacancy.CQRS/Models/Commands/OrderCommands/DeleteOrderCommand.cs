using MediatR;

namespace WebApiServiceForVacancy.CQRS.Models.Commands.OrderCommands;

public class DeleteOrderCommand : IRequest<bool>
{
    public DeleteOrderCommand(uint id)
    {
        Id = id;
    }

    public uint Id { get; set; }
}