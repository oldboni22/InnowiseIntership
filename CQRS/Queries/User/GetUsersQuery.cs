using MediatR;
using Shared.Input.Request;
using Shared.Output;

namespace CQRS.Queries.User;

public record GetUsersQuery(UserRequestParameters Parameters) : IRequest<(IEnumerable<UserDto>,PagedListMetaData)>;