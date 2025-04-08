using System.Text.Json;
using Exceptions;
using InnowiseIntership.ActionFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.Input.Creation;
using Shared.Input.PagingParameters;
using Shared.Input.Update;
using Shared.Output;

namespace InnowiseIntership.ApiControllers;


[Route("api/Users")]
[ApiController]
[Authorize("CRUD:users")]
public class UserController(IServiceManager service) : ControllerBase
{
    private readonly IServiceManager _service = service;

    [HttpGet]
    public async Task<IActionResult> GetUsersAsync([FromQuery] UserRequestParameters parameters)
    {
        var pagedResult = await _service.User.GetUsersAsync(false,parameters);
        
        Response.Headers.Add("X-Pagination",JsonSerializer.Serialize(pagedResult.metaData));
        return Ok(pagedResult.users);
    }

    [HttpGet("{id:int}", Name = "GetUserById")]
    public async Task<IActionResult> GetUserByIdAsync(int id)
    {
        var user = await _service.User.GetUserByIdAsync(id, false);
        return Ok(user);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilter))]
    public async Task<IActionResult> CreateUserAsync([FromBody] UserCreationDto userDto)
    {
        var created = await _service.User.CreateUserAsync(userDto);
        return CreatedAtRoute("GetUserById", new { id = created.Id }, created);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUserAsync(int id)
    {
        await _service.User.DeleteUserAsync(id);
        return NoContent();
    }

    [HttpPut("{id:int}")]
    [ServiceFilter(typeof(ValidationFilter))]
    public async Task<IActionResult> UpdateUserAsync(int id,[FromBody] UserForUpdateDto userDto)
    {
        await _service.User.UpdateUserAsync(id, userDto);
        return NoContent();
    }

}