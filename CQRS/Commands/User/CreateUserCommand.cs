using MediatR;
using Shared.Input.Creation;
using Shared.Output;

namespace CQRS.Commands.User;

public record CreateUserCommand(UserCreationDto UserCreationDto) : IRequest<UserDto>;