using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApiServiceForVacancy.CQRS.Models.Commands.OrderCommands;
using WebApiServiceForVacancy.Data;
using WebApiServiceForVacancy.Data.Entities;

namespace WebApiServiceForVacancy.CQRS.Handlers.CommandHandlers.OrderCommandHandlers;

public class CreateNewOrderCommandHandler : IRequestHandler<CreateNewOrderCommand, bool>
{
    private readonly WebApiServiceForVacancyContext _database;
    private readonly IMapper _mapper;

    public CreateNewOrderCommandHandler(WebApiServiceForVacancyContext database, IMapper mapper)
    {
        _database = database;
        _mapper = mapper;
    }

    public async Task<bool> Handle(CreateNewOrderCommand command, CancellationToken cancellationToken)
    {

        var order = _mapper.Map<Order>(command);

        await _database.Orders.AddAsync(order, cancellationToken: cancellationToken);

        await _database.SaveChangesAsync(cancellationToken: cancellationToken);

        return true;
    }
}