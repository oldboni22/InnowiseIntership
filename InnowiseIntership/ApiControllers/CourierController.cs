using Exceptions;
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
    public async Task<IActionResult> GetCourierByIdAsync(int id)
    {
        var courier = await _service.Courier.GetCourierByIdAsync(id,false);
        return Ok(courier);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourierAsync([FromBody] CourierCreationDto courier)
    {
        if (courier == null)
            throw new CourierCreationDtoBadRequestException();

        var created =  await _service.Courier.CreateCourierAsync(courier);
        return CreatedAtRoute("GetByIdAsync", new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateCourierAsync(int id, [FromBody] CourierForUpdateDto courier)
    {
        if (courier == null)
            throw new CourierUpdateDtoBadRequestException();

        await _service.Courier.UpdateCourierAsync(id, courier);
        return NoContent();
    }
}