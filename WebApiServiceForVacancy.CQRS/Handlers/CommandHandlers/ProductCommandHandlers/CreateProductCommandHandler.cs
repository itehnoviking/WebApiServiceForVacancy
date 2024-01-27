using AutoMapper;
using MediatR;
using WebApiServiceForVacancy.CQRS.Models.Commands.ProductCommands;
using WebApiServiceForVacancy.Data;
using WebApiServiceForVacancy.Data.Entities;

namespace WebApiServiceForVacancy.CQRS.Handlers.CommandHandlers.ProductCommandHandlers;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, bool>
{
    private readonly WebApiServiceForVacancyContext _database;
    private readonly IMapper _mapper;

    public async Task<bool> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(request);

        await _database.Products.AddAsync(product, cancellationToken: cancellationToken);

        await _database.SaveChangesAsync(cancellationToken: cancellationToken);

        return true;
    }
}