using System.Linq.Dynamic.Core;
using Domain.Entities;

namespace Repository.Extensions;

public static class RepositoryUserExtensions
{
    public static IQueryable<User> Sort(this  IQueryable<User> users, string queryString)
    {
        if (string.IsNullOrEmpty(queryString))
            return users.OrderBy(u => u.FirstName);
        
        var orderQuery = OrderQueryBuilder.CreateQuery<User>(queryString);
        
        if(string.IsNullOrEmpty(orderQuery))
            return users.OrderBy(u => u.FirstName);
        
        return users.OrderBy(orderQuery);
    }
}