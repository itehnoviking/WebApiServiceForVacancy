using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApiServiceForVacancy.Core.DTOs;
using WebApiServiceForVacancy.CQRS.Models.Queries.OrderQueries;
using WebApiServiceForVacancy.Data;

namespace WebApiServiceForVacancy.CQRS.Handlers.QueryHandlers.OrderQueryHandler;

public class GetOrderByNameAndCreateDateQueryHandler : IRequestHandler<GetOrderByNameAndCreateDateQuery, OrderDto>
{
    private readonly WebApiServiceForVacancyContext _database;
    private readonly IMapper _mapper;

    public GetOrderByNameAndCreateDateQueryHandler(WebApiServiceForVacancyContext database, IMapper mapper)
    {
        _database = database;
        _mapper = mapper;
    }

    public async Task<OrderDto> Handle(GetOrderByNameAndCreateDateQuery query, CancellationToken cancellationToken)
    {
        var order = await _database.Orders
            .AsNoTracking()
            .Where(order => order.Number.Equals(query.Number) & order.CreateDateTime.Equals(query.CreateDateTime) & order.CustomerId.Equals(query.CustomerId))
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        return _mapper.Map<OrderDto>(order);
    }
}