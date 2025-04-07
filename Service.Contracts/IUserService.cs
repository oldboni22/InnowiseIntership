using Shared.Input;
using Shared.Input.Creation;
using Shared.Input.PagingParameters;
using Shared.Input.Update;
using Shared.Output;

namespace Service.Contracts;

public interface IUserService
{
    Task<(IEnumerable<UserDto>users,PagedListMetaData metaData)> GetUsersAsync(bool trackChanges,UserRequestParameters parameters);
    Task<UserDto> GetUserByIdAsync(int id,bool trackChanges);
    Task<UserDto> CreateUserAsync(UserCreationDto user);
    Task DeleteUserAsync(int id);
    Task UpdateUserAsync(int id,UserForUpdateDto user);
}