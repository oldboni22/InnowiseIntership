using Exceptions;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.Input.Creation;

namespace InnowiseIntership.ApiControllers;


[Route("api/Reviews")]
[ApiController]
public class ReviewController(IServiceManager service) : ControllerBase
{
    private IServiceManager _service = service;

    [HttpGet("User/{userId:int}")]
    public async Task<IActionResult> GetUserReviewsAsync(int userId)
    {
        var reviews = await _service.Review.GetUserReviewsAsync(userId, false);
        return Ok(reviews);
    }
    
    [HttpGet("Courier/{courierId:int}")]
    public async Task<IActionResult> GetCourierReviewsAsync(int courierId)
    {
        var reviews = await _service.Review.GetCourierReviewsAsync(courierId, false);
        return Ok(reviews);
    }

    [HttpGet("{userId,courierId,id}")]
    public async Task<IActionResult> GetReviewByIdAsync(int userId,int courierId,int id)
    {
        var review = await _service.Review.GetReviewByIdAsync(userId, courierId, id,false);
        return Ok(review);
    }
        
    [HttpPost("{userId,courierId}")]
    public async Task<IActionResult> CreateReviewAsync(int userId,int courierId,
        [FromBody] ReviewCreationDto review)
    {
        if (review == null)
            throw new ReviewCreationDtoBadRequestException();
        
        var created = await _service.Review.CreateReviewAsync(userId, courierId, review);
        return CreatedAtRoute("GetById", new { userId, courierId, created.Id }, created);
    }
    
    
}