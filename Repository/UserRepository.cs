using Domain.Entities;
using Repository.Contracts;

namespace Repository;

public class UserRepository(RepositoryContext context) : RepositoryBase<User>(context),IUserRepository
{
    
}