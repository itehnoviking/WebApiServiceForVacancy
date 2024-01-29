using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Collections.Generic;
using System.Net;
using WebApiServiceForVacancy.Core.DTOs;
using WebApiServiceForVacancy.Core.Interfaces.Services;
using WebApiServiceForVacancy.Data.Entities;
using WebApiServiceForVacancy.Models.Requests;
using WebApiServiceForVacancy.Models.Responses;

namespace WebApiServiceForVacancy.Controllers;

[Route("api/[controller]")]
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

    [HttpGet("{customerId}")]
    [ProducesResponseType(typeof(IEnumerable<GetOrderByCustomerIdResponce>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ResponseMessage), 500)]
    public async Task<IActionResult> GetOrdersByCustomerId(uint customerId)
    {
        try
        {
            var orderDtos = await _orderService.GetOrdersByCustomerId(customerId);

            if (orderDtos == null || !orderDtos.Any())
            {
                return BadRequest(new ResponseMessage { Message = "No content" });
            }

            return Ok(_mapper.Map<IEnumerable<GetOrderByCustomerIdResponce>>(orderDtos));
        }
        catch (Exception ex)
        {
            Log.Error($"{ex.Message}. {Environment.NewLine} {ex.StackTrace}");
            return StatusCode(500, $"Failed to get orders: {ex.Message}");
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ResponseMessage), 500)]
    public async Task<IActionResult> CreateNew([FromBody] CreateNewOrderRequest request)
    {
        try
        {
            if (request == null || !ModelState.IsValid)
            {
                return BadRequest(new ResponseMessage { Message = "Request is null or invalid" });
            }

            await _orderService.CreateNewOrder(_mapper.Map<CreateNewOrderDto>(request));
            return Ok(new ResponseMessage { Message = "New order is created" });
        }
        catch (Exception ex)
        {
            Log.Error($"{ex.Message}. {Environment.NewLine} {ex.StackTrace}");
            return StatusCode(500, $"Failed to create order: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ResponseMessage), 500)]
    public async Task<IActionResult> Delete(uint id)
    {
        try
        {
            await _orderService.DeleteOrder(id);
            return Ok(new ResponseMessage { Message = "Order is deleted" });
        }
        catch (Exception ex)
        {
            Log.Error($"{ex.Message}. {Environment.NewLine} {ex.StackTrace}");
            return StatusCode(500, $"Failed to delete order: {ex.Message}");
        }
    }
}