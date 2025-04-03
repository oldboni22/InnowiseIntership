using Shared.Input;
using Shared.Input.Creation;
using Shared.Output;

namespace Service.Contracts;

public interface ICourierService
{
    Task<CourierDto> GetCourierByIdAsync(int id, bool trackChanges);
    Task CreateCourierAsync(CourierCreationDto courier);
    Task UpdateCourier(int id,CourierForUpdateDto courier);
}