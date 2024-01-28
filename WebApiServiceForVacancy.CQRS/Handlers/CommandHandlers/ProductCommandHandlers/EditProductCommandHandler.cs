using AutoMapper;
using MediatR;
using WebApiServiceForVacancy.CQRS.Models.Commands.ProductCommands;
using WebApiServiceForVacancy.Data;
using WebApiServiceForVacancy.Data.Entities;

namespace WebApiServiceForVacancy.CQRS.Handlers.CommandHandlers.ProductCommandHandlers;

public class EditProductCommandHandler : IRequestHandler<EditProductCommand, bool>
{
    private readonly WebApiServiceForVacancyContext _database;
    private readonly IMapper _mapper;

    public EditProductCommandHandler(WebApiServiceForVacancyContext database, IMapper mapper)
    {
        _database = database;
        _mapper = mapper;
    }
    public async Task<bool> Handle(EditProductCommand command, CancellationToken cancellationToken)
    {
        var productEntity = _mapper.Map<Product>(command);

        _database.Products.Update(productEntity);

        await _database.SaveChangesAsync(cancellationToken: cancellationToken);

        return true;
    }
}