using Domain.Entities;

namespace Repository.Contracts;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetUsersAsync(bool trackChanges);
    Task<User?> GetUserByIdAsync(int id, bool trackChanges);
    void CreateUser(User user);
    void DeleteUser(User user);

}