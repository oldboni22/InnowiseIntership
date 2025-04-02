using AutoMapper;
using Domain.Entities;
using Exceptions.NotFound;
using Repository.Contracts;
using Service.Contracts;
using Shared.Input.Creation;
using Shared.Input.Update;
using Shared.Output;

namespace Service;

public class UserService(IRepositoryManager repositoryManager, IMapper mapper) : IUserService
{
    private readonly IRepositoryManager _repositoryManager = repositoryManager;
    private readonly IMapper _mapper = mapper;

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

    public async Task CreateUser(UserCreationDto user)
    {
        var entity = _mapper.Map<User>(user);
        
        _repositoryManager.User.CreateUser(entity);
        await _repositoryManager.SaveAsync();
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
        await _repositoryManager.SaveAsync();

    }
}