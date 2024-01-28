using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApiServiceForVacancy.Core.DTOs;
using WebApiServiceForVacancy.CQRS.Models.Queries.ProductQueries;
using WebApiServiceForVacancy.Data;

namespace WebApiServiceForVacancy.CQRS.Handlers.QueryHandlers.ProductQueryHandlers;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
{
    private readonly WebApiServiceForVacancyContext _database;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(WebApiServiceForVacancyContext database, IMapper mapper)
    {
        _database = database;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await _database.Products
            .AsNoTracking()
            .Where(product => product.Id.Equals(query.Id))
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        return _mapper.Map<ProductDto>(product);
    }
}