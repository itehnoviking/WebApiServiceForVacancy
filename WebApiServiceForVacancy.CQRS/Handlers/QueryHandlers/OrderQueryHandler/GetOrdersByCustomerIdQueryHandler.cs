using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApiServiceForVacancy.Core.DTOs;
using WebApiServiceForVacancy.CQRS.Models.Queries.OrderQueries;
using WebApiServiceForVacancy.Data;

namespace WebApiServiceForVacancy.CQRS.Handlers.QueryHandlers.OrderQueryHandler;

public class GetOrdersByCustomerIdQueryHandler : IRequestHandler<GetOrdersByCustomerIdQuery, IEnumerable<OrderDto>>
{
    private readonly WebApiServiceForVacancyContext _database;
    private readonly IMapper _mapper;

    public GetOrdersByCustomerIdQueryHandler(WebApiServiceForVacancyContext database, IMapper mapper)
    {
        _database = database;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderDto>> Handle(GetOrdersByCustomerIdQuery query, CancellationToken cancellationToken)
    {
        var orders = await _database.Orders
            .AsNoTracking()
            .Where(order => order.CustomerId.Equals(query.CustomerId))
            .Select(order => _mapper.Map<OrderDto>(order))
            .ToListAsync(cancellationToken: cancellationToken);

        return orders;
    }
}