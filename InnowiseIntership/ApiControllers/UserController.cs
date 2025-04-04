using Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.Input.Creation;
using Shared.Input.Update;
using Shared.Output;

namespace InnowiseIntership.ApiControllers;


[Route("api/Users")]
[ApiController]
public class UserController(IServiceManager service) : ControllerBase
{
    private readonly IServiceManager _service = service;

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _service.User.GetUsersAsync(false);
        return Ok(users);
    }

    [HttpGet("{id:int}", Name = "GetUserById")]
    public async Task<IActionResult> GetUserByIdAsync(int id)
    {
        var user = await _service.User.GetUserByIdAsync(id, false);
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUserAsync([FromBody] UserCreationDto user)
    {
        if (user == null)
            throw new UserCreationDtoBadRequestException();
        
        var created = await _service.User.CreateUserAsync(user);
        return CreatedAtRoute("GetUserById", new { id = created.Id }, created);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUserAsync(int id)
    {
        await _service.User.DeleteUserAsync(id);
        return NoContent();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateUserAsync(int id,[FromBody] UserForUpdateDto user)
    {
        if (user == null)
            throw new UserUpdateDtoBadRequestException();
        await _service.User.UpdateUserAsync(id, user);
        return NoContent();
    }

}