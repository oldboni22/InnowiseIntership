using Domain.Entities;

namespace Repository.Contracts;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetOrdersAsync(bool trackChanges);
    Task<Order?> GetOrderByIdAsync(int id, bool trackChanges);
    void CreateOrder(Order order);
    void DeleteOrder(Order order);
}