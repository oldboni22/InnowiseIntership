using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace Repository;

public class ReviewRepository(RepositoryContext context) : RepositoryBase<Review>(context),IReviewRepository
{
    public async Task<IEnumerable<Review>> GetUserReviewsAsync(int userId,bool trackChanges) =>
        await FindByCondition(review => review.UserId == userId,trackChanges)
            .OrderBy(review => review.Rating)
            .ToListAsync();
    public async Task<IEnumerable<Review>> GetCourierReviewsAsync(int courierId,bool trackChanges) =>
        await FindByCondition(review => review.CourierId == courierId,trackChanges)
            .OrderBy(review => review.Rating)
            .ToListAsync();

    public async Task<Review?> GetReviewByIdAsync(int userId,int courierId,int id, bool trackChanges) =>
        await FindByCondition(review => review.UserId == userId 
                                        && review.CourierId == courierId 
                                        && review.Id == id, trackChanges)
            .SingleOrDefaultAsync();

    public void CreateReview(Review review) => Create(review);

    public void DeleteReview(Review review) => Delete(review);
}