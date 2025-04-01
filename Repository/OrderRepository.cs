using Domain.Entities;
using Repository.Contracts;

namespace Repository;

public class OrderRepository(RepositoryContext context) : RepositoryBase<Order>(context),IOrderRepository
{
    
}