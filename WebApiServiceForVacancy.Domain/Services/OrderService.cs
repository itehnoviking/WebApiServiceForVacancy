using MediatR;
using WebApiServiceForVacancy.Core.DTOs;
using WebApiServiceForVacancy.Core.Interfaces.Services;
using WebApiServiceForVacancy.CQRS.Models.Commands.OrderCommands;
using WebApiServiceForVacancy.CQRS.Models.Commands.OrderProductCommands;
using WebApiServiceForVacancy.CQRS.Models.Commands.ProductCommands;
using WebApiServiceForVacancy.CQRS.Models.Queries.OrderQueries;

namespace WebApiServiceForVacancy.Domain.Services;

public class OrderService : IOrderService
{
    private readonly IMediator _mediator;

    public OrderService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task CreateNewOrder(CreateNewOrderDto dto)
    {
        try
        {
            var command = new CreateNewOrderCommand(dto);

            await _mediator.Send(command, new CancellationToken());
            await SetOrderProductAsync(command);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex + ex.Message);
            //_logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task DeleteOrder(uint id)
    {
        try
        {
            var command = new DeleteOrderCommand(id);
            await _mediator.Send(command, new CancellationToken());
            await DeleteOrderProductAsync(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex + ex.Message);
            //_logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<IEnumerable<OrderDto>> GetOrdersByCustomerId(uint customerId)
    {
        try
        {
            var listOrderByCustomerDto = await _mediator.Send(new GetOrdersByCustomerIdQuery(customerId), new CancellationToken());

            return listOrderByCustomerDto;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex + ex.Message);
            //_logger.LogError(ex, ex.Message);
            throw;
        }
    }

    private async Task SetOrderProductAsync(CreateNewOrderCommand command)
    {
        var order = await _mediator.Send(new GetOrderByNameAndCreateDateQuery(command),  new CancellationToken());

        await _mediator.Send(new SetOrderProductCommand(order.Id, command.ProductIds), new CancellationToken());
    }

    private async Task DeleteOrderProductAsync(uint orderId)
    {
        await _mediator.Send(new DeleteOrderProductCommand(orderId), new CancellationToken());
    }
}