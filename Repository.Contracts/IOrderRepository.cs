using Domain.Entities;
using Shared.Input.Request;
using Shared.Output;

namespace Repository.Contracts;

public interface IOrderRepository
{
    Task<PagedList<Order>> GetOrdersAsync(int userId,bool trackChanges,OrderRequestParameters parameters);
    Task<PagedList<Order>> GetPendingOrdersAsync(bool trackChanges,OrderRequestParameters parameters);
    Task<Order?> GetOrderByIdAsync(int userId,int id, bool trackChanges);
    void CreateOrder(Order order);
}