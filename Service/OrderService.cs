using AutoMapper;
using Domain.Entities;
using Exceptions.NotFound;
using Repository.Contracts;
using Service.Contracts;
using Shared.Input.Creation;
using Shared.Output;

namespace Service;

public class OrderService(IRepositoryManager repositoryManager, IMapper mapper) : IOrderService
{
    private readonly IRepositoryManager _repositoryManager = repositoryManager;
    private readonly IMapper _mapper = mapper;
    public async Task<IEnumerable<OrderDto>> GetOrdersAsync(int userId, bool trackChanges)
    {
        var user = await _repositoryManager.User.GetUserByIdAsync(userId, false);
        if (user == null)
            throw new UserNotFoundException(userId);

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
        var user = await _repositoryManager.User.GetUserByIdAsync(userId, false);
        if (user == null)
            throw new UserNotFoundException(userId);

        var order = _repositoryManager.Order.GetOrderByIdAsync(userId, id, trackChanges);
        if (order != null)
            throw new OrderNotFoundException(id);
        
        var result = _mapper.Map<OrderDto>(order);
        return result;
    }

    public async Task CreateOrder(int userId, OrderCreationDto order)
    {
        var user = await _repositoryManager.User.GetUserByIdAsync(userId, false);
        if (user == null)
            throw new UserNotFoundException(userId);

        var entity = _mapper.Map<Order>(order);
        _repositoryManager.Order.CreateOrder(entity);
        
        await _repositoryManager.SaveAsync();
    }

    public async Task DeleteOrder(int userId, int id)
    {
        var user = await _repositoryManager.User.GetUserByIdAsync(userId, false);
        if (user == null)
            throw new UserNotFoundException(userId);

        var order = await _repositoryManager.Order.GetOrderByIdAsync(userId,id, false);
        if (order == null)
            throw new OrderNotFoundException(id);
        
        _repositoryManager.Order.DeleteOrder(order);
        await _repositoryManager.SaveAsync();
    }
}