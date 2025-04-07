using System.Collections;
using Shared.Input.Creation;
using Shared.Input.PagingParameters;
using Shared.Output;

namespace Service.Contracts;

public interface IReviewService
{
    Task<(IEnumerable<ReviewDto> reviews,PagedListMetaData metaData)> GetUserReviewsAsync(int userId,bool trackChanges
    ,ReviewRequestParameters  parameters);
    Task<(IEnumerable<ReviewDto> reviews,PagedListMetaData metaData)> GetCourierReviewsAsync(int courierId,bool trackChanges
    ,ReviewRequestParameters  parameters); 
    Task<ReviewDto> GetReviewByIdAsync(int userId,int courierId,int id,bool trackChanges);
    Task<ReviewDto> CreateReviewAsync(int userId, int courierId, ReviewCreationDto review);
    Task DeleteReviewAsync(int userId, int courierId, int id);
    
}