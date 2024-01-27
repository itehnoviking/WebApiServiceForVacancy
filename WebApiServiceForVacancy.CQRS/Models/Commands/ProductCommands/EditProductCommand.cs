using MediatR;

namespace WebApiServiceForVacancy.CQRS.Models.Commands.ProductCommands;

public class EditProductCommand : IRequest<bool>
{
    public uint Id { get; set; }
    public string Name { get; set; }
    public bool IsAvailable { get; set; }
}