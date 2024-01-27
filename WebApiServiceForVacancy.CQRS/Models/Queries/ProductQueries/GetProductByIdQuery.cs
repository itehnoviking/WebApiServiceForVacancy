using MediatR;
using WebApiServiceForVacancy.Core.DTOs;

namespace WebApiServiceForVacancy.CQRS.Models.Queries.ProductQueries;

public class GetProductByIdQuery : IRequest<ProductDto>
{
    public GetProductByIdQuery(uint id)
    {
        Id = id;
    }

    public uint Id { get; set; }
}