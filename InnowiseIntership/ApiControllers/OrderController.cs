using System.Text.Json;
using InnowiseIntership.ActionFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.Input.Creation;
using Shared.Input.Request;
using Shared.Input.Update;

namespace InnowiseIntership.ApiControllers;

[Route("api/Orders")]
[ApiController]
[Authorize]
public class OrderController(IServiceManager service) : ControllerBase
{
    private readonly IServiceManager _service = service;
    
    [HttpGet]
    [ServiceFilter(typeof(ValidationFilter))]
    public async Task<IActionResult> GetPendingOrdersAsync([FromQuery] OrderRequestParameters parameters)
    {
        var pagedResult = await _service.Order.GetPendingOrdersAsync(false,parameters);
        
        Response.Headers.Add("X-Pagination",JsonSerializer.Serialize(pagedResult.metaData));
        return Ok(pagedResult.orders); 
    }
    
    [HttpGet("{userId:int}")]
    [ServiceFilter(typeof(ValidationFilter))]
    public async Task<IActionResult> GetOrdersAsync(int userId,[FromQuery] OrderRequestParameters parameters)
    {
        var pagedResult = await _service.Order.GetOrdersAsync(userId,false,parameters);
        
        Response.Headers.Add("X-Pagination",JsonSerializer.Serialize(pagedResult.metaData));
        return Ok(pagedResult.orders); 
    }

    [HttpGet("{userId},{orderId}")]
    public async Task<IActionResult> GetOrderByIdAsync(int userId, int orderId)
    {
        var result = await _service.Order.GetOrderByIdAsync(userId, orderId,false);
        return Ok(result);
    }
    
    [HttpPost("{userId:int}")]
    [ServiceFilter(typeof(ValidationFilter))]
    public async Task<IActionResult> CreateOrderAsync(int  userId,[FromBody] OrderCreationDto order)
    {
        var result = await _service.Order.CreateOrderAsync(userId, order);
        return Ok(result);
    }

    [HttpPut("{userId},{orderId}")]
    [ServiceFilter(typeof(ValidationFilter))]
    public async Task<IActionResult> UpdateOrderAsync(int userId,int orderId,[FromBody] OrderForUpdateDto order)
    {
        await _service.Order.UpdateOrderAsync(userId, orderId, order);
        return NoContent();
    }
    
}