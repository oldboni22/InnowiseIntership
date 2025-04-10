using Domain.Entities;
using System.Linq.Dynamic.Core;

namespace Repository.Extensions;

public static class RepositoryOrderExtensions
{
    public static IQueryable<Order> Sort(this  IQueryable<Order> orders, string queryString)
    {
        if (string.IsNullOrEmpty(queryString))
            return orders.OrderBy(o => o.CreatedAt);
        
        var orderQuery = OrderQueryBuilder.CreateQuery<Order>(queryString);
        
        if(string.IsNullOrEmpty(orderQuery))
            return orders.OrderBy(o => o.CreatedAt);
        
        return orders.OrderBy(orderQuery);
    }
}