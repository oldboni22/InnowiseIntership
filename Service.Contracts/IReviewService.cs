using Shared.Input.Creation;
using Shared.Output;

namespace Service.Contracts;

public interface IReviewService
{
    Task<IEnumerable<ReviewDto>> GetUserReviewsAsync(int userId,bool trackChanges);
    Task<IEnumerable<ReviewDto>> GetCourierReviewsAsync(int courierId,bool trackChanges); 
    Task<ReviewDto> GetReviewByIdAsync(int userId,int courierId,int id,bool trackChanges);
    Task<ReviewDto> CreateReviewAsync(int userId, int courierId, ReviewCreationDto review);
    Task DeleteReviewAsync(int userId, int courierId, int id);
    
}