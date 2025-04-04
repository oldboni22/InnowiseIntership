using Shared.Input.Creation;
using Shared.Input.Update;
using Shared.Output;

namespace Service.Contracts;

public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetOrdersAsync(int userId,bool trackChanges);
    Task<IEnumerable<OrderDto>> GetPendingOrdersAsync(bool trackChanges);
    Task<OrderDto> GetOrderByIdAsync(int userId,int id, bool trackChanges);
    Task<OrderDto> CreateOrderAsync(int userId, OrderCreationDto order);
    Task DeleteOrderAsync(int userId,int id);
    Task UpdateOrderAsync(int userId,int id,OrderForUpdateDto order);
}