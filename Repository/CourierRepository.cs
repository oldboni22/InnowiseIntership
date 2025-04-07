using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;
using Shared.Input.PagingParameters;

namespace Repository;

public class CourierRepository(RepositoryContext context) : RepositoryBase<Courier>(context),ICourierRepository
{
    public async Task<IEnumerable<Courier>> GetCouriersAsync(bool trackChanges) =>
        await FindAll(trackChanges)
            .OrderBy(courier => courier.Name)
            .ToListAsync();

    public async Task<Courier?> GetCourierByIdAsync(int id, bool trackChanges) =>
        await FindByCondition(courier => courier.Id == id, trackChanges)
            .SingleOrDefaultAsync();

    public void CreateCourier(Courier courier) => Create(courier);
    public void DeleteCourier(Courier courier) => Delete(courier);
}