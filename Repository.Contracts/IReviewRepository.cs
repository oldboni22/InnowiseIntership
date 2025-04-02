using Domain.Entities;

namespace Repository.Contracts;

public interface IReviewRepository
{
    Task<IEnumerable<Review>> GetReviewsAsync(bool trackChanges);
    Task<Review?> GetReviewByIdAsync(int id, bool trackChanges);
    void CreateReview(Review review);
    void DeleteReview(Review review);
}