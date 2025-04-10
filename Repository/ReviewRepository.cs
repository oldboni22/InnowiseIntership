using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;
using Shared.Input.Request;
using Shared.Output;

namespace Repository;

public class ReviewRepository(RepositoryContext context) : RepositoryBase<Review>(context),IReviewRepository
{
    public async Task<PagedList<Review>> GetUserReviewsAsync(int userId, bool trackChanges
        ,ReviewRequestParameters parameters)
    {
        var reviews = await FindByCondition(review => review.UserId == userId, trackChanges)
            .OrderBy(review => review.Rating)
            .ToListAsync();
        
        return PagedList<Review>.ToPagedList(reviews,parameters.PageNumber, parameters.PageSize);
    }

    public async Task<PagedList<Review>> GetCourierReviewsAsync(int courierId, bool trackChanges
        , ReviewRequestParameters parameters)
    {
        var reviews = await FindByCondition(review => review.CourierId == courierId, trackChanges)
            .OrderBy(review => review.Rating)
            .ToListAsync();
        
        return PagedList<Review>.ToPagedList(reviews,parameters.PageNumber, parameters.PageSize);
    }

    public async Task<Review?> GetReviewByIdAsync(int userId,int courierId,int id, bool trackChanges) =>
        await FindByCondition(review => review.UserId == userId 
                                        && review.CourierId == courierId 
                                        && review.Id == id, trackChanges)
            .SingleOrDefaultAsync();

    public void CreateReview(Review review) => Create(review);

    public void DeleteReview(Review review) => Delete(review);
}