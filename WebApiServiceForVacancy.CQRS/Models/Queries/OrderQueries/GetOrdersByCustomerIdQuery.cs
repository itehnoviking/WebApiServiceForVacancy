using MediatR;
using WebApiServiceForVacancy.Core.DTOs;

namespace WebApiServiceForVacancy.CQRS.Models.Queries.OrderQueries;

public class GetOrdersByCustomerIdQuery : IRequest<IEnumerable<OrderDto>>
{
    public GetOrdersByCustomerIdQuery(uint customerId)
    {
        CustomerId = customerId;
    }
    public uint CustomerId { get; set; }
}