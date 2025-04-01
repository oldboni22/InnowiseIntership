using Domain.Entities;
using Repository.Contracts;

namespace Repository;

public class CourierRepository(RepositoryContext context) : RepositoryBase<Courier>(context),ICourierRepository
{
    
}