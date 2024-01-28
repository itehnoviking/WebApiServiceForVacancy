using AutoMapper;
using MediatR;
using WebApiServiceForVacancy.Core.DTOs;
using WebApiServiceForVacancy.Core.Interfaces.Services;
using WebApiServiceForVacancy.CQRS.Models.Commands.ProductCommands;
using WebApiServiceForVacancy.CQRS.Models.Queries.ProductQueries;

namespace WebApiServiceForVacancy.Domain.Services;

public class ProductService : IProductService
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public ProductService(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<ProductDto> GetByIdAsync(uint id)                                     
    {
        try
        {
            var product = await _mediator.Send(new GetProductByIdQuery(id), new CancellationToken());

            return product;

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex + ex.Message);
            //_logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAvailableForOrderAsync()
    {
        try
        {
            var products = await _mediator.Send(new GetAllProductsAvailableForOrderQuery(), new CancellationToken());

            return products;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex + ex.Message);
            //_logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task CreateAsync(CreateNewProductDto dto)
    {
        try
        {
            var command = new CreateProductCommand(dto);

            await _mediator.Send(command, new CancellationToken());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex + ex.Message);
            //_logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task EditAsync(ProductDto dto)
    {
        try
        {
            var command = _mapper.Map<EditProductCommand>(dto);

            await _mediator.Send(command, new CancellationToken());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex + ex.Message);
            //_logger.LogError(ex, ex.Message);
            throw;
        }
    }
}