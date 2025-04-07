using Domain.Entities;
using Shared.Input.PagingParameters;
using Shared.Output;

namespace Repository.Contracts;

public interface IUserRepository
{
    Task<PagedList<User>> GetUsersAsync(bool trackChanges,UserRequestParameters parameters);
    Task<User?> GetUserByIdAsync(int id, bool trackChanges);
    Task<User?> GetUserByEmailAsync(string email, bool trackChanges);
    void CreateUser(User user);
    void DeleteUser(User user);

}