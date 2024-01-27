using MediatR;
using WebApiServiceForVacancy.Core.DTOs;

namespace WebApiServiceForVacancy.CQRS.Models.Queries.ProductQueries;

public class GetAllProductsAvailableForOrderQuery : IRequest<IEnumerable<ProductDto>>
{
    
}