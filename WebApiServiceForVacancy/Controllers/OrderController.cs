using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiServiceForVacancy.Core.DTOs;
using WebApiServiceForVacancy.Core.Interfaces.Services;
using WebApiServiceForVacancy.Models.Requests;
using WebApiServiceForVacancy.Models.Responses;

namespace WebApiServiceForVacancy.Controllers;

[Route("[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    public OrderController(IOrderService orderService, IMapper mapper)
    {
        _orderService = orderService;
        _mapper = mapper;
    }

    [HttpGet("GetOrdersByCustomerId/{customerId}")]
    public async Task<IActionResult> GetOrdersByCustomerId(uint customerId)
    {
        try
        {
            var orderDtos = await _orderService.GetOrdersByCustomerId(customerId);

            return Ok(_mapper.Map<IEnumerable<GetOrderByCustomerIdResponce>>(orderDtos));
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to create order: {ex.Message}");
        }
    }

    [HttpPost("CreateNew")]
    public async Task<IActionResult> CreateNew([FromBody] CreateNewOrderRequest request)
    {
        try
        {
            await _orderService.CreateNewOrder(_mapper.Map<CreateNewOrderDto>(request));
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to create order: {ex.Message}");
        }
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(uint id)
    {
        try
        {
            await _orderService.DeleteOrder(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to create order: {ex.Message}");
        }
    }
}