using AutoMapper;
using Domain.Entities;
using Exceptions.NotFound;
using Repository.Contracts;
using Service.Contracts;
using Shared.Input.Creation;
using Shared.Input.Update;
using Shared.Output;

namespace Service;

public class OrderService(IRepositoryManager repositoryManager, IMapper mapper) : IOrderService
{
    private readonly IRepositoryManager _repositoryManager = repositoryManager;
    private readonly IMapper _mapper = mapper;

    private async Task<Order> TryGetOrderByIdAsync(int userId,int id,bool trackChanges)
    {
        var order = await _repositoryManager.Order.GetOrderByIdAsync(userId, id,trackChanges);
        if (order != null)
            throw new OrderNotFoundException(id);
        
        return order;
    }

    private async Task<User> TryGetUserByIdAsync(int userId, bool trackChanges)
    {
        var user = await _repositoryManager.User.GetUserByIdAsync(userId, false);
        if (user == null)
            throw new UserNotFoundException(userId);
        return user;
    }
    
    public async Task<IEnumerable<OrderDto>> GetOrdersAsync(int userId, bool trackChanges)
    {
        var user = await TryGetUserByIdAsync(userId, trackChanges);

        var orders = await _repositoryManager.Order.GetOrdersAsync(userId, trackChanges);
        var result = _mapper.Map<IEnumerable<OrderDto>>(orders);

        return result;
    }

    public async Task<IEnumerable<OrderDto>> GetPendingOrdersAsync(bool trackChanges)
    {
        var orders = await _repositoryManager.Order.GetPendingOrdersAsync(trackChanges);
        var result = _mapper.Map<IEnumerable<OrderDto>>(orders);

        return result;
    }

    public async Task<OrderDto> GetOrderByIdAsync(int userId, int id, bool trackChanges)
    {
        var user = await TryGetUserByIdAsync(userId, trackChanges);
        var order = await TryGetOrderByIdAsync(userId,id,trackChanges);
        
        var result = _mapper.Map<OrderDto>(order);
        return result;
    }

    public async Task<OrderDto> CreateOrderAsync(int userId, OrderCreationDto order)
    {
        var user = await TryGetUserByIdAsync(userId, false);

        var entity = _mapper.Map<Order>(order);
        entity = entity with
        {
            CreatedAt = DateTime.UtcNow
        };
        
        _repositoryManager.Order.CreateOrder(entity);
        
        await _repositoryManager.SaveAsync();

        return _mapper.Map<OrderDto>(entity);
    }

    public async Task DeleteOrderAsync(int userId, int id)
    {
        var user = await TryGetUserByIdAsync(userId, false);
        var order = await TryGetOrderByIdAsync(userId,id,false);
        
        _repositoryManager.Order.DeleteOrder(order);
        await _repositoryManager.SaveAsync();
    }

    public async Task UpdateOrderAsync(int userId, int id, OrderForUpdateDto order)
    {
        var user = await TryGetUserByIdAsync(userId, false);
        var entity = await TryGetOrderByIdAsync(userId,id,false);

        _mapper.Map(order, entity);
        await _repositoryManager.SaveAsync();
    }
}