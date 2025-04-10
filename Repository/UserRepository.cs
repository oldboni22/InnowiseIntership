using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;
using Shared.Input.Request;
using Shared.Output;

namespace Repository;

public class UserRepository(RepositoryContext context) : RepositoryBase<User>(context),IUserRepository
{
    public async Task<PagedList<User>> GetUsersAsync(bool trackChanges, UserRequestParameters parameters)
    {
        var users = await FindAll(trackChanges)
            .OrderBy(user => user.FirstName)
            .ToListAsync();
        
        return PagedList<User>.ToPagedList(users, parameters.PageNumber, parameters.PageSize);
    }

    public async Task<User?> GetUserByIdAsync(int id, bool trackChanges) =>
        await FindByCondition(user => user.Id == id,false)
            .SingleOrDefaultAsync();

    public async Task<User?> GetUserByEmailAsync(string email, bool trackChanges) =>
        await FindByCondition(user => user.Email == email,false)
            .SingleOrDefaultAsync();
        
    

    public void CreateUser(User user) => Create(user);

    public void DeleteUser(User user) => Delete(user);
}