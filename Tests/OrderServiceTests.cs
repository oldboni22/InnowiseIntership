using AutoMapper;
using Domain.Entities;
using Moq;
using Repository.Contracts;
using Service;
using Shared.Output;

namespace Tests;

public class OrderServiceTests
{
    private readonly Mock<IRepositoryManager> _repositoryManagerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly OrderService _orderService;

    public OrderServiceTests()
    {
        _repositoryManagerMock = new Mock<IRepositoryManager>();
        _mapperMock = new Mock<IMapper>();
        _orderService = new OrderService(_repositoryManagerMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task GetOrderByIdAsync_OrderDto()
    {
        int userId = 1;
        int courierId = 1;
        int orderId = 1;
        
        SetUpUser(userId);
        SetUpCourier(courierId);    

        var order = new Order
        {
            Id = orderId,
            CourierId = courierId,
            UserId = userId,
            Address = "Address",
            Status = "Shipping",
            Description = ""
        };
        var orderDto = new OrderDto(orderId,"Address","Shipping");
        
        _repositoryManagerMock.Setup(r 
            => r.Order.GetOrderByIdAsync(userId,orderId,false)).ReturnsAsync(order);
        _mapperMock.Setup(m 
            => m.Map<OrderDto>(order)).Returns(orderDto);
        
        
        var result = await _orderService.GetOrderByIdAsync(userId, orderId,false);
        
        Assert.Equal(orderId, result.Id);
        Assert.Equal(order.Address, result.Address);
        Assert.Equal("Shipping", result.Status);
    }
    

    private void SetUpUser(int userId)
    {
        var user = new User
        {
            Id = userId,
            FirstName = "John",
            LastName = "Doe",
            Email = "johnDoe@test.com",
            PasswordHash = "123", PasswordSalt = "123"

        };
        _repositoryManagerMock.Setup(r 
            => r.User.GetUserByIdAsync(userId,false)).ReturnsAsync(user);
    }
    private void SetUpCourier(int courierId)
    {
        var courier = new Courier
        {
            Id = courierId,
            Name = "John",
            Vehicle = "Govnovoz"
        };
        _repositoryManagerMock.Setup(r 
            => r.Courier.GetCourierByIdAsync(courierId,false)).ReturnsAsync(courier);
    }
}