using AutoMapper;
using CQRS.Commands.User;
using MediatR;
using Repository.Contracts;
using Shared.Input.Creation;
using Shared.Output;

namespace CQRS.Handlers.User;

public class CreateUserHandler(IRepositoryManager repositoryManager,IMapper mapper)
    : IRequestHandler<CreateUserCommand,UserDto>
{
    private readonly IRepositoryManager _repositoryManager = repositoryManager;
    private readonly IMapper _mapper = mapper;

    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<Domain.Entities.User>(request.UserCreationDto);
        _repositoryManager.User.CreateUser(user);

        await _repositoryManager.SaveAsync();

        return _mapper.Map<UserDto>(user);
    }
}   