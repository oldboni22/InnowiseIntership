using Shared.Input;
using Shared.Input.Creation;
using Shared.Output;

namespace Service.Contracts;

public interface ICourierService
{
    Task<CourierDto> GetCourierByIdAsync(int id, bool trackChanges);
    Task<CourierDto> CreateCourierAsync(CourierCreationDto courier);
    Task UpdateCourierAsync(int id,CourierForUpdateDto courier);
}