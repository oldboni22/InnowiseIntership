using Domain.Entities;

namespace Repository.Contracts;

public interface IReviewRepository
{
    Task<IEnumerable<Review>> GetUserReviewsAsync(int userId,bool trackChanges);
    Task<IEnumerable<Review>> GetCourierReviewsAsync(int courierId,bool trackChanges);
    Task<Review?> GetReviewByIdAsync(int userId,int courierId,int id, bool trackChanges);
    void CreateReview(Review review);
    void DeleteReview(Review review);
}