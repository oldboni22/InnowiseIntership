using MediatR;
using Shared.Input.Request;
using Shared.Output;

namespace CQRS.Queries.User;

public record GetUsersQuery(UserRequestParameters parameters) : IRequest<(IEnumerable<UserDto>,PagedListMetaData)>;