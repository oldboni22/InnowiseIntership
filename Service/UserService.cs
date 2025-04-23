using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Domain.Entities;
using Exceptions.AlreadyExists;
using Exceptions.NotFound;
using Repository.Contracts;
using Service.Contracts;
using Shared.Input;
using Shared.Input.Creation;
using Shared.Input.Request;
using Shared.Input.Update;
using Shared.Output;

namespace Service;

public class UserService(IRepositoryManager repositoryManager, IMapper mapper) : IUserService
{
    private readonly IRepositoryManager _repositoryManager = repositoryManager;
    private readonly IMapper _mapper = mapper;

    private async Task<bool> CheckIfUserExistsByEmailAsync(string email)
    {
        var user = await _repositoryManager.User.GetUserByEmailAsync(email, false);
        return user != null;
    }
    private async Task<User> TryGetUserByIdAsync(int id,bool trackChanges)
    {
        var user = await _repositoryManager.User.GetUserByIdAsync(id, trackChanges);
        if (user == null)
            throw new UserNotFoundException(id);
        return user;
    }
    
    private string GeneratePasswordHash(string password, out string salt)
    {
        var bytes = RandomNumberGenerator.GetBytes(32);
        salt = Convert.ToBase64String(bytes);

        string hash;
        using (var sha = SHA256.Create())
        {
            bytes = Encoding.UTF8.GetBytes(password + salt);
            hash = Convert.ToBase64String(sha.ComputeHash(bytes));
        }

        
        
        return hash;
    }
    
    public async Task<(IEnumerable<UserDto>, PagedListMetaData)> GetUsersAsync(bool trackChanges,UserRequestParameters parameters)
    {
        var pagedUsers = await _repositoryManager.User.GetUsersAsync(trackChanges,parameters);
        
        var users = _mapper.Map<IEnumerable<UserDto>>(pagedUsers);
        return (users, pagedUsers.MetaData);
    }

    public async Task<UserDto> GetUserByIdAsync(int id, bool trackChanges)
    {
        var user = await TryGetUserByIdAsync(id,false);
        
        var result = _mapper.Map<UserDto>(user);
        return result;
    }

    public async Task<UserDto> CreateUserAsync(UserCreationDto user)
    {
       if(await CheckIfUserExistsByEmailAsync(user.Email) is true)
           throw new UserAlreadyExistsException(user.Email);
       
       var entity = _mapper.Map<User>(user);
        
        entity = entity with
        {
            PasswordHash = GeneratePasswordHash(user.Password,out var salt),
            PasswordSalt = salt
        };
        
        _repositoryManager.User.CreateUser(entity);
        await _repositoryManager.SaveAsync();

        return _mapper.Map<UserDto>(entity);
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await TryGetUserByIdAsync(id,false);
        
        _repositoryManager.User.DeleteUser(user);
        await _repositoryManager.SaveAsync();
    }

    public async Task UpdateUserAsync(int id, UserForUpdateDto user)
    {
        var entity = await TryGetUserByIdAsync(id,true);
        _mapper.Map(user, entity);

        entity = entity with
        {
            PasswordHash = GeneratePasswordHash(user.Password,out var salt),
            PasswordSalt = salt
        };
        
        await _repositoryManager.SaveAsync();
    }
}