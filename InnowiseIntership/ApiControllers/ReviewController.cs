using System.Text.Json;
using Exceptions;
using InnowiseIntership.ActionFilters;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.Input.Creation;
using Shared.Input.Request;

namespace InnowiseIntership.ApiControllers;


[Route("api/Reviews")]
[ApiController]
[Authorize]
public class ReviewController(IServiceManager service) : ControllerBase
{
    private IServiceManager _service = service;

    [HttpGet("User/{userId:int}")]
    [HttpCacheExpiration(CacheLocation = CacheLocation.Private)]
    public async Task<IActionResult> GetUserReviewsAsync(int userId, [FromQuery]  ReviewRequestParameters parameters)
    {
        var reviews = await _service.Review.GetUserReviewsAsync(userId, false,parameters);
        
        Response.Headers.Add("X-Pagination",JsonSerializer.Serialize(reviews.metaData));
        return Ok(reviews.reviews);
    }
    
    [HttpGet("Courier/{courierId:int}")]
    [HttpCacheExpiration(CacheLocation = CacheLocation.Private)]
    public async Task<IActionResult> GetCourierReviewsAsync(int courierId, [FromQuery] ReviewRequestParameters parameters)
    {
        var reviews = await _service.Review.GetCourierReviewsAsync(courierId, false,parameters);
        
        Response.Headers.Add("X-Pagination",JsonSerializer.Serialize(reviews.metaData));
        return Ok(reviews.reviews);
    }

    [HttpGet("{userId,courierId,id}")]
    [HttpCacheExpiration(CacheLocation = CacheLocation.Private)]
    public async Task<IActionResult> GetReviewByIdAsync(int userId,int courierId,int id)
    {
        var review = await _service.Review.GetReviewByIdAsync(userId, courierId, id,false);
        return Ok(review);
    }
        
    [HttpPost("{userId,courierId}")]
    [ServiceFilter(typeof(ValidationFilter))]
    public async Task<IActionResult> CreateReviewAsync(int userId,int courierId,
        [FromBody] ReviewCreationDto reviewDto)
    {
        var created = await _service.Review.CreateReviewAsync(userId, courierId, reviewDto);
        return CreatedAtRoute("GetById", new { userId, courierId, created.Id }, created);
    }
}