using CQRS.Commands.User;
using MediatR;
using Repository.Contracts;

namespace CQRS.Handlers.User;

public class DeleteUserHandler(IRepositoryManager repositoryManager) : IRequestHandler<DeleteUserCommand,Unit>
{
    private readonly IRepositoryManager _repositoryManager = repositoryManager;
    
    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _repositoryManager.User.GetUserByIdAsync(request.UserId, false);
        
        _repositoryManager.User.DeleteUser(user!);
        await _repositoryManager.SaveAsync(); 
        
        return Unit.Value;
    }
}