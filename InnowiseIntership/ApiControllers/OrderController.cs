using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.Input.Request;

namespace InnowiseIntership.ApiControllers;

[Route("api/Orders")]
[ApiController]
[Authorize]
public class OrderController(IServiceManager service) : ControllerBase
{
    private readonly IServiceManager _service = service;
    
    [HttpGet("{userId:int}")]
    public async Task<IActionResult> GetOrdersAsync(int userId,[FromQuery] OrderRequestParameters parameters)
    {
        var pagedResult = await _service.Order.GetOrdersAsync(userId,false,parameters);
        
        Response.Headers.Add("X-Pagination",JsonSerializer.Serialize(pagedResult.metaData));
        return Ok(pagedResult.orders); 
    }
    
    
    
    
}