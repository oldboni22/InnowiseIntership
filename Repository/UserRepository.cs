using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace Repository;

public class UserRepository(RepositoryContext context) : RepositoryBase<User>(context),IUserRepository
{
    public async Task<IEnumerable<User>> GetUsersAsync(bool trackChanges) => 
        await FindAll(trackChanges)
            .OrderBy(user => user.FirstName)
            .ToListAsync();
    public async Task<User?> GetUserByIdAsync(int id, bool trackChanges) =>
        await FindByCondition(user => user.Id == id,false)
            .SingleOrDefaultAsync();
    public void CreateUser(User user) => Create(user);

    public void DeleteUser(User user) => Delete(user);
}