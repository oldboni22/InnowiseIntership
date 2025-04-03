using Shared.Input.Creation;
using Shared.Output;

namespace Service.Contracts;

public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetOrdersAsync(int userId,bool trackChanges);
    Task<IEnumerable<OrderDto>> GetPendingOrdersAsync(bool trackChanges);
    Task<OrderDto> GetOrderByIdAsync(int userId,int id, bool trackChanges);
    Task CreateOrder(int userId, OrderCreationDto order);
    Task DeleteOrder(int userId,int id);
}