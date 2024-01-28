using MediatR;
using WebApiServiceForVacancy.Core.DTOs;

namespace WebApiServiceForVacancy.CQRS.Models.Commands.OrderCommands;

public class CreateNewOrderCommand : IRequest<bool>
{

    public CreateNewOrderCommand(CreateNewOrderDto dto)
    {
        Number = dto.Number;
        CreateDateTime = dto.CreateDateTime;
        CustomerId = dto.CustomerId;
        ProductIds = dto.ProductIds;
    }

    public string Number { get; set; }
    public DateTime CreateDateTime { get; set; }
    public uint CustomerId { get; set; }

    public IEnumerable<uint> ProductIds { get; set; }
}