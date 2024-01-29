using System.Collections.Concurrent;
using AutoMapper;
using MediatR;
using Serilog;
using WebApiServiceForVacancy.CQRS.Models.Commands.OrderProductCommands;
using WebApiServiceForVacancy.Data;
using WebApiServiceForVacancy.Data.Entities;

namespace WebApiServiceForVacancy.CQRS.Handlers.CommandHandlers.OrderProductCommandHandlers;

public class SetOrderProductCommandHandler : IRequestHandler<SetOrderProductCommand, bool>
{
    private readonly WebApiServiceForVacancyContext _database;
    private readonly IMapper _mapper;

    public SetOrderProductCommandHandler(WebApiServiceForVacancyContext database, IMapper mapper)
    {
        _database = database;
        _mapper = mapper;
    }

    public async Task<bool> Handle(SetOrderProductCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var bagOrderProduct = new ConcurrentBag<OrderProduct>();

            Parallel.ForEach(command.ProductIds,
                productId => bagOrderProduct.Add(new OrderProduct() { OrderId = command.OrderId, ProductId = productId }));

            await _database.OrderProducts
                .AddRangeAsync(bagOrderProduct, cancellationToken: cancellationToken);

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