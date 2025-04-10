using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;
using Shared.Input.Request;
using Shared.Output;

namespace Repository;

public class OrderRepository(RepositoryContext context) : RepositoryBase<Order>(context),IOrderRepository
{
    public async Task<PagedList<Order>> GetOrdersAsync(int userId, bool trackChanges,
        OrderRequestParameters parameters)
    {
        var orders = await FindByCondition(order => order.UserId == userId
                && order.OrderStatus == parameters.OrderStatus,false)
            .OrderBy(order => order.OrderStatus)
            .ToListAsync();
        
        return PagedList<Order>.ToPagedList(orders, parameters.PageNumber, parameters.PageSize);
    }
    
    public async Task<Order?> GetOrderByIdAsync(int userId,int id, bool trackChanges) =>
        await FindByCondition(order => order.UserId == userId && order.Id == id,trackChanges)
            .SingleOrDefaultAsync();

    public void CreateOrder(Order order) => Create(order);
    

}