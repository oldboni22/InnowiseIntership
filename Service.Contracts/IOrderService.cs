using Shared.Input.Creation;
using Shared.Input.PagingParameters;
using Shared.Input.Update;
using Shared.Output;

namespace Service.Contracts;

public interface IOrderService
{
    Task<(IEnumerable<OrderDto> orders, PagedListMetaData metaData)> GetOrdersAsync(int userId,bool trackChanges
        ,OrderRequestParameters  parameters);
    Task<(IEnumerable<OrderDto> orders, PagedListMetaData metaData)> GetPendingOrdersAsync(bool trackChanges
        ,OrderRequestParameters  parameters);
    Task<OrderDto> GetOrderByIdAsync(int userId,int id, bool trackChanges);
    Task<OrderDto> CreateOrderAsync(int userId, OrderCreationDto order);
    Task UpdateOrderAsync(int userId,int id,OrderForUpdateDto order);
}