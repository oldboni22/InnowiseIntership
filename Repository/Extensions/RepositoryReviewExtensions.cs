using Domain.Entities;
using System.Linq.Dynamic.Core;

namespace Repository.Extensions;

public static class RepositoryReviewExtensions
{
    public static IQueryable<Review> Sort(this  IQueryable<Review> reviews, string queryString)
    {
        if (string.IsNullOrEmpty(queryString))
            return reviews.OrderBy(r => r.Rating);
        
        var orderQuery = OrderQueryBuilder.CreateQuery<Review>(queryString);
        
        if(string.IsNullOrEmpty(orderQuery))
            return reviews.OrderBy(r => r.Rating);
        
        return reviews.OrderBy(orderQuery);
    }
}