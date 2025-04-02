using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace Repository;

public class ReviewRepository(RepositoryContext context) : RepositoryBase<Review>(context),IReviewRepository
{
    public async Task<IEnumerable<Review>> GetReviewsAsync(bool trackChanges) =>
        await FindAll(trackChanges)
            .OrderBy(review => review.Rating)
            .ToListAsync();

    public async Task<Review?> GetReviewByIdAsync(int id, bool trackChanges) =>
        await FindByCondition(review => review.Id == id, trackChanges)
            .SingleOrDefaultAsync();

    public void CreateReview(Review review) => Create(review);

    public void DeleteReview(Review review) => Delete(review);
}