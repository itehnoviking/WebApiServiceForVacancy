using MediatR;
using WebApiServiceForVacancy.Core.DTOs;
using WebApiServiceForVacancy.CQRS.Models.Commands.OrderCommands;

namespace WebApiServiceForVacancy.CQRS.Models.Queries.OrderQueries;

public class GetOrderByNameAndCreateDateQuery : IRequest<OrderDto>
{
    public GetOrderByNameAndCreateDateQuery(CreateNewOrderCommand command)
    {
        Number = command.Number;
        CreateDateTime = command.CreateDateTime;
        CustomerId = command.CustomerId;
    }
    public string Number { get; set; }
    public DateTime CreateDateTime { get; set; }
    public uint CustomerId { get; set; }
}