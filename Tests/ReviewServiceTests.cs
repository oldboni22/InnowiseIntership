using AutoMapper;
using Domain.Entities;
using Moq;
using Repository.Contracts;
using Service;

namespace Tests;

public class ReviewServiceTests
{
    private readonly Mock<IRepositoryManager> _repositoryManagerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly ReviewService _reviewService;

    public ReviewServiceTests()
    {
        _repositoryManagerMock = new Mock<IRepositoryManager>();
        _mapperMock = new Mock<IMapper>();
        _reviewService = new ReviewService(_repositoryManagerMock.Object, _mapperMock.Object);
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