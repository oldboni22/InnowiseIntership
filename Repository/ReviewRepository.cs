using Domain.Entities;
using Repository.Contracts;

namespace Repository;

public class ReviewRepository(RepositoryContext context) : RepositoryBase<Review>(context),IReviewRepository
{
    
}