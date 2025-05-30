using Exceptions;
using InnowiseIntership.ActionFilters;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.Input;
using Shared.Input.Creation;

namespace InnowiseIntership.ApiControllers;

[Route("api/Couriers")]
[Authorize]
public class CourierController(IServiceManager service) : ControllerBase
{
    private readonly IServiceManager _service = service;

    [HttpGet("{id:int}", Name = "GetCourierById")]
    [HttpCacheExpiration(CacheLocation = CacheLocation.Private)]
    public async Task<IActionResult> GetCourierByIdAsync(int id)
    {
        var courier = await _service.Courier.GetCourierByIdAsync(id,false);
        return Ok(courier);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilter))]
    public async Task<IActionResult> CreateCourierAsync([FromBody] CourierCreationDto courierDto)
    {
        var created =  await _service.Courier.CreateCourierAsync(courierDto);
        return CreatedAtRoute("GetByIdAsync", new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [ServiceFilter(typeof(ValidationFilter))]
    public async Task<IActionResult> UpdateCourierAsync(int id, [FromBody] CourierForUpdateDto courierDto)
    {
        await _service.Courier.UpdateCourierAsync(id, courierDto);
        return NoContent();
    }
}