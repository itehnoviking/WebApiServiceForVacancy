using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using WebApiServiceForVacancy.CQRS.Models.Commands.OrderCommands;
using WebApiServiceForVacancy.Data;

namespace WebApiServiceForVacancy.CQRS.Handlers.CommandHandlers.OrderCommandHandlers;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
{
    private readonly WebApiServiceForVacancyContext _database;
    public DeleteOrderCommandHandler(WebApiServiceForVacancyContext database)
    {
        _database = database;
    }

    public async Task<bool> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var order = await _database.Orders
                .Where(order => order.Id.Equals(command.Id))
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            _database.Orders.Remove(order);
            await _database.SaveChangesAsync(cancellationToken: cancellationToken);

            return true;
        }
        catch (Exception ex)
        {
            Log.Error($"{ex.Message}. {Environment.NewLine} {ex.StackTrace}");
            throw new InvalidOperationException(ex.Message);
        }
    }
}