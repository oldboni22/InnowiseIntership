using MediatR;

namespace CQRS.Commands.User;

public record DeleteUserCommand(int UserId) : IRequest<Unit>
{
    
}