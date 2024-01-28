using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApiServiceForVacancy.CQRS.Models.Commands.OrderProductCommands;
using WebApiServiceForVacancy.Data;

namespace WebApiServiceForVacancy.CQRS.Handlers.CommandHandlers.OrderProductCommandHandlers;

public class DeleteOrderProductCommandHandler : IRequestHandler<DeleteOrderProductCommand, bool>
{
    private readonly WebApiServiceForVacancyContext _database;

    public DeleteOrderProductCommandHandler(WebApiServiceForVacancyContext database)
    {
        _database = database;
    }

    public async Task<bool> Handle(DeleteOrderProductCommand command, CancellationToken cancellationToken)
    {
        var orderProducts = await _database.OrderProducts
            .Where(op => op.OrderId.Equals(command.OrderId))
            .ToListAsync(cancellationToken: cancellationToken);

        _database.OrderProducts.RemoveRange(orderProducts);
        await _database.SaveChangesAsync(cancellationToken: cancellationToken);

        return true;
    }
}