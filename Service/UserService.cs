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
using Shared.Input.Update;
using Shared.Output;

namespace Service;

public class UserService(IRepositoryManager repositoryManager, IMapper mapper) : IUserService
{
    private readonly IRepositoryManager _repositoryManager = repositoryManager;
    private readonly IMapper _mapper = mapper;

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
    
    public async Task<IEnumerable<UserDto>> GetUsersAsync(bool trackChanges)
    {
        var users = await _repositoryManager.User.GetUsersAsync(trackChanges);
        
        var result = _mapper.Map<IEnumerable<UserDto>>(users);
        return result;
    }

    public async Task<UserDto> GetUserByIdAsync(int id, bool trackChanges)
    {
        var user = await _repositoryManager.User.GetUserByIdAsync(id, false);
        if (user == null)
            throw new UserNotFoundException(id);
        
        var result = _mapper.Map<UserDto>(user);
        return result;
    }

    public async Task<UserDto> CreateUserAsync(UserCreationDto user)
    {
        var userEntity = await _repositoryManager.User.GetUserByEmailAsync(user.Email, false);
        if ( userEntity != null)
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
        var user = await _repositoryManager.User.GetUserByIdAsync(id, false);
        if(user == null)
            throw new UserNotFoundException(id);
        
        _repositoryManager.User.DeleteUser(user);
        await _repositoryManager.SaveAsync();
    }

    public async Task UpdateUserAsync(int id, UserForUpdateDto user)
    {
        var entity = await _repositoryManager.User.GetUserByIdAsync(id, true);
        if(entity == null)
            throw new UserNotFoundException(id);
        
        _mapper.Map(user, entity);

        entity = entity with
        {
            PasswordHash = GeneratePasswordHash(user.Password,out var salt),
            PasswordSalt = salt
        };
        
        await _repositoryManager.SaveAsync();
    }
}