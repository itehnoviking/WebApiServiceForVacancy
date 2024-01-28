using WebApiServiceForVacancy.Core.DTOs;

namespace WebApiServiceForVacancy.Core.Interfaces.Services;

public interface IOrderService
{
    Task CreateNewOrder(CreateNewOrderDto dto);
    Task DeleteOrder(uint id);
    Task<IEnumerable<OrderDto>> GetOrdersByCustomerId(uint customerId);

}