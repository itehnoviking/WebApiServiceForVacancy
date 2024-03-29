﻿using AutoMapper;
using MediatR;
using Serilog;
using WebApiServiceForVacancy.CQRS.Models.Commands.ProductCommands;
using WebApiServiceForVacancy.Data;
using WebApiServiceForVacancy.Data.Entities;

namespace WebApiServiceForVacancy.CQRS.Handlers.CommandHandlers.ProductCommandHandlers;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, bool>
{
    private readonly WebApiServiceForVacancyContext _database;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(WebApiServiceForVacancyContext database, IMapper mapper)
    {
        _database = database;
        _mapper = mapper;
    }

    public async Task<bool> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var product = _mapper.Map<Product>(command);

            await _database.Products.AddAsync(product, cancellationToken: cancellationToken);

            await _database.SaveChangesAsync(cancellationToken: cancellationToken);

            return true;
        }
        catch (Exception ex)
        {
            Log.Error($"{ex.Message}. {Environment.NewLine} {ex.StackTrace}");
            throw new InvalidOperationException(ex.Message);
        }
    }
}