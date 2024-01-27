using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebApiServiceForVacancy.Core.DTOs;
using WebApiServiceForVacancy.CQRS.Models.Queries.ProductQueries;
using WebApiServiceForVacancy.Data;

namespace WebApiServiceForVacancy.CQRS.Handlers.QueryHandlers.ProductQueryHandlers;

public class GetAllProductsAvailableForOrderQueryHandler : IRequestHandler<GetAllProductsAvailableForOrderQuery, IEnumerable<ProductDto>>
{
    private readonly WebApiServiceForVacancyContext _database;
    private readonly IMapper _mapper;


    public GetAllProductsAvailableForOrderQueryHandler(WebApiServiceForVacancyContext database, IMapper mapper)
    {
        _database = database;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsAvailableForOrderQuery request, CancellationToken cancellationToken)
    {
        var products = await _database.Products
            .AsNoTracking()
            .Where(product => product.IsAvailable.Equals(true))
            .Select(product => _mapper.Map<ProductDto>(product))
            .ToListAsync(cancellationToken: cancellationToken);

        return products;
    }
}