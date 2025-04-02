using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace Repository;

public class OrderRepository(RepositoryContext context) : RepositoryBase<Order>(context),IOrderRepository
{
    public async Task<IEnumerable<Order>> GetOrdersAsync(bool trackChanges) =>
        await FindAll(trackChanges)
            .OrderBy(order => order.Status)
            .ToListAsync();


    public async Task<Order?> GetOrderByIdAsync(int id, bool trackChanges) =>
        await FindByCondition(order => order.Id == id,trackChanges)
            .SingleOrDefaultAsync();

    public void CreateOrder(Order order) => Create(order);

    public void DeleteOrder(Order order) => Delete(order);
}