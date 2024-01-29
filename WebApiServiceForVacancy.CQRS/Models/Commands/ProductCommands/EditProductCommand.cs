using MediatR;
using WebApiServiceForVacancy.Core.DTOs;
using WebApiServiceForVacancy.Data.Entities;

namespace WebApiServiceForVacancy.CQRS.Models.Commands.ProductCommands;

public class EditProductCommand : IRequest<bool>
{
    public EditProductCommand(ProductDto dto)
    {
        Id = dto.Id;
        Name = dto.Name;
        IsAvailable = dto.IsAvailable;
    }

    public uint Id { get; set; }
    public string Name { get; set; }
    public bool IsAvailable { get; set; }
}