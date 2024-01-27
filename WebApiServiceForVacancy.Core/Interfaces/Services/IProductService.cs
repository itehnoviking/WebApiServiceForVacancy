using WebApiServiceForVacancy.Core.DTOs;

namespace WebApiServiceForVacancy.Core.Interfaces.Services;

public interface IProductService
{
    Task<ProductDto> GetByIdAsync(uint id);
    Task CreateAsync(CreateNewProductDto dto);
    Task<IEnumerable<ProductDto>> GetAllProductsAvailableForOrderAsync();
    Task EditAsync(ProductDto dto);
}