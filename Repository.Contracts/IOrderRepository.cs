using Domain.Entities;

namespace Repository.Contracts;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetOrdersAsync(int userId,bool trackChanges);
    Task<IEnumerable<Order>> GetPendingOrdersAsync(bool trackChanges);
    Task<Order?> GetOrderByIdAsync(int userId,int id, bool trackChanges);
    void CreateOrder(Order order);
    void DeleteOrder(Order order);
}