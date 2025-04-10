using AutoMapper;
using Domain.Entities;
using Exceptions.AlreadyExists;
using Exceptions.NotFound;
using Moq;
using Repository.Contracts;
using Service;
using Shared.Input.Creation;
using Shared.Input.Request;
using Shared.Input.Update;
using Shared.Output;

namespace Tests;

public class UserServiceTests
{
    private readonly Mock<IRepositoryManager> _repositoryManagerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _repositoryManagerMock = new Mock<IRepositoryManager>();
        _mapperMock = new Mock<IMapper>();
        _userService = new UserService(_repositoryManagerMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task GetUsersAsync_ReturnsUsersDtoAndPagedListMetaData()
    {
        
        var users = new List<User>
        {
            new User
            {
                Id = 1, 
                FirstName = "John",
                LastName = "Doe", 
                Email = "johnDoe@test.com",
                PasswordHash = "123",PasswordSalt = "123"
                
            },
            
            new User
            {
                Id = 2, 
                FirstName = "John2",
                LastName = "Doe2", 
                Email = "johnDoe2@test.com",
                PasswordHash = "123",PasswordSalt = "123"
                
            },
        };
        var pagedUsers = PagedList<User>.ToPagedList(users, 1, 10);
        var usersDto = new List<UserDto>
        {
            new UserDto(1,"John Doe","johnDoe@test.com"),
            new UserDto(2,"John2 Doe2","johnDoe2@test.com"),
        };
        
        _repositoryManagerMock.Setup(r => 
                r.User.GetUsersAsync(false, It.IsAny<UserRequestParameters>()))
            .ReturnsAsync(pagedUsers);
        
        _mapperMock.Setup(m => m.Map<IEnumerable<UserDto>>(It.IsAny<IEnumerable<User>>()))
            .Returns(usersDto);
        
        var parameters = new UserRequestParameters()
        {
            PageSize = 10,
            PageNumber = 1  
        };
        var (result, metaData) = await _userService.GetUsersAsync(false,parameters);

        var resultUsers = result.ToList();
        
        Assert.NotEmpty(resultUsers);
        Assert.Equal(2,resultUsers.Count);
        Assert.Equal(1,metaData.CurrentPage);
        Assert.Equal(10,metaData.PageSize);
    }

    [Fact]
    public async Task GetUserByIdAsync_ReturnsUserDto()
    {
        int userId = 1;
        var user = new User
        {
            Id = userId, 
            FirstName = "John",
            LastName = "Doe", 
            Email = "johnDoe@test.com",
            PasswordHash = "123",PasswordSalt = "123"
        };
        
        var userDto = new UserDto(1, "John Doe", "johnDoe@test.com");
        
        _repositoryManagerMock.Setup(r => r.User.GetUserByIdAsync(userId, false))
            .ReturnsAsync(user);
        _mapperMock.Setup(m => m.Map<UserDto>(user))
            .Returns(userDto);
        
        
        var result = await _userService.GetUserByIdAsync(userId,false);
        
        
        Assert.NotNull(result);
        Assert.Equal(userId,result.Id);
        Assert.Equal("John Doe", result.FullName);
    }

    [Fact]
    public async Task GetUserByIdAsync_ThrowsUserNotFoundException()
    {
        int userId = 3;
        _repositoryManagerMock.Setup(r 
                => r.User.GetUserByIdAsync(userId, false))
            .ReturnsAsync(()=>null);
        
        await Assert.ThrowsAsync<UserNotFoundException>(() => _userService.GetUserByIdAsync(userId, false));
    }
    

    [Fact]
    public async Task CreateUserAsync_ReturnsUserDto()
    {   
        var userCreationDto = new UserCreationDto 
        { 
            FirstName = "John", 
            LastName = "Doe",
            Email = "johnDoe@test.com", 
            Password = "123" 
        };
    
        var user = new User 
        { 
            Id = 1, 
            FirstName = "John", 
            LastName = "Doe",
            Email = "johnDoe@test.com",
            PasswordHash = "123",
            PasswordSalt = "123"
        };
    
        var userDto = new UserDto(1, "John Doe", "johnDoe@test.com");

        _repositoryManagerMock.Setup(r 
                => r.User.GetUserByEmailAsync(userCreationDto.Email, false))
            .ReturnsAsync(() => null);
        _mapperMock.Setup(m => m.Map<User>(userCreationDto))
            .Returns(user);
        _mapperMock.Setup(m => m.Map<UserDto>(It.IsAny<User>()))
            .Returns(userDto);
        _repositoryManagerMock.Setup(r => r.SaveAsync())
            .Returns(Task.CompletedTask);
        
        var result = await _userService.CreateUserAsync(userCreationDto);
        
        Assert.Equal(1, result.Id);
        Assert.Equal("John Doe", result.FullName);
        _repositoryManagerMock.Verify(r => r.User.CreateUser(It.Is<User>(u => 
            u.Email == userCreationDto.Email && 
            string.IsNullOrEmpty(u.PasswordHash) == false && 
            string.IsNullOrEmpty(u.PasswordSalt) == false)), Times.Once);
        _repositoryManagerMock.Verify(r => r.SaveAsync(), Times.Once);
    }


    [Fact]
    public async Task CreateUserAsync_ThrowsUserAlreadyExistsException()
    {
        var userCreationDto = new UserCreationDto 
        { 
            FirstName = "John", 
            LastName = "Doe",
            Email = "johnDoe@test.com", 
            Password = "123" 
        };
    
        var existingUser = new User 
        { 
            Id = 1, 
            FirstName = "John", 
            LastName = "Doe",
            Email = "johnDoe@test.com",
            PasswordHash = "123",
            PasswordSalt = "123"
        };
        
        _repositoryManagerMock.Setup(r 
                => r.User.GetUserByEmailAsync(userCreationDto.Email, false))
            .ReturnsAsync(existingUser);
        
         await Assert.ThrowsAsync<UserAlreadyExistsException>(() => _userService.CreateUserAsync(userCreationDto));
    }
    
    
    [Fact]
    public async Task DeleteUserAsync()
    {
        var userId = 1;
        var user = new User
        { 
            Id = 1, 
            FirstName = "John", 
            LastName = "Doe",
            Email = "johnDoe@test.com",
            PasswordHash = "123",
            PasswordSalt = "123"
        };
    
        _repositoryManagerMock.Setup(r 
                => r.User.GetUserByIdAsync(userId, false))
            .ReturnsAsync(user);
        _repositoryManagerMock.Setup(r 
                => r.SaveAsync())
            .Returns(Task.CompletedTask);
        
        await _userService.DeleteUserAsync(userId);
        
        _repositoryManagerMock.Verify(r => r.User.DeleteUser(user), Times.Once);
        _repositoryManagerMock.Verify(r => r.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteUserAsync_ThrowsUserNotFoundException()
    {
        
        var userId = 1;
        _repositoryManagerMock.Setup(r => r.User.GetUserByIdAsync(userId, false))
            .ReturnsAsync(() => null);
        
        await Assert.ThrowsAsync<UserNotFoundException>(() => _userService.DeleteUserAsync(userId));
    }
    
    [Fact]
    public async Task UpdateUserAsync()
    {
        var userId = 1;
        var existingUser = new User 
        { 
            Id = 1, 
            FirstName = "John", 
            LastName = "Doe",
            Email = "johnDoe@test.com",
            PasswordHash = "123",
            PasswordSalt = "123"
        };
    
        var updateDto = new UserForUpdateDto(
        
            FirstName : "CoolJohn", 
            LastName : "CoolDoe",
            Email : "johnDoeCool@test.com",
            Password : "newPassword"
        );
    
        _repositoryManagerMock.Setup(r 
                => r.User.GetUserByIdAsync(userId, true))
            .ReturnsAsync(existingUser);
        _repositoryManagerMock.Setup(r 
                => r.SaveAsync())
            .Returns(Task.CompletedTask);
        
        await _userService.UpdateUserAsync(userId, updateDto);
        
        _repositoryManagerMock.Verify(r => r.User.GetUserByIdAsync(userId, true), Times.Once);
        _mapperMock.Verify(m => m.Map(updateDto,existingUser), Times.Once);
        _repositoryManagerMock.Verify(r => r.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateUserAsync_ThrowsUserNotFoundException()
    {
        var userId = 1;
        var updateDto = new UserForUpdateDto(
        
            FirstName : "CoolJoe", 
            LastName : "CoolDoe",
            Email : "johnDoeCool@test.com",
            Password : "newPassword"
        );
        _repositoryManagerMock.Setup(r 
                => r.User.GetUserByIdAsync(userId, true))
            .ReturnsAsync(() => null);
        
        await Assert.ThrowsAsync<UserNotFoundException>(() => _userService.UpdateUserAsync(userId, updateDto));
    }
    
}