using Domain.Entities;
using Shared.Input.PagingParameters;
using Shared.Output;

namespace Repository.Contracts;

public interface IReviewRepository
{
    Task<PagedList<Review>> GetUserReviewsAsync(int userId,bool trackChanges,ReviewRequestParameters parameters);
    Task<PagedList<Review>> GetCourierReviewsAsync(int courierId,bool trackChanges,ReviewRequestParameters parameters);
    Task<Review?> GetReviewByIdAsync(int userId,int courierId,int id, bool trackChanges);
    void CreateReview(Review review);
    void DeleteReview(Review review);
}