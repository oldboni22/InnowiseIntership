using Domain.Entities;
using Shared.Input.PagingParameters;

namespace Repository.Contracts;

public interface ICourierRepository
{
    Task<IEnumerable<Courier>> GetCouriersAsync(bool trackChanges);
    Task<Courier?> GetCourierByIdAsync(int id, bool trackChanges);
    void CreateCourier(Courier courier);
    void DeleteCourier(Courier courier);
}