using MediatR;
using WebApiServiceForVacancy.Core.DTOs;

namespace WebApiServiceForVacancy.CQRS.Models.Commands.ProductCommands;

public class CreateProductCommand : IRequest<bool>
{
    public CreateProductCommand(CreateNewProductDto dto)
    {
        Name = dto.Name;
        IsAvailable = dto.IsAvailable;
    }

    public string Name { get; set; }
    public bool IsAvailable { get; set; }
}