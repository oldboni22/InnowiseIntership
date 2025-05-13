using AutoMapper;
using CQRS.Queries.User;
using MediatR;
using Repository.Contracts;
using Shared.Output;

namespace CQRS.Handlers.User;

public class GetUsersHandler(IRepositoryManager repositoryManager,IMapper mapper) 
    : IRequestHandler<GetUsersQuery,(IEnumerable<UserDto>,PagedListMetaData)>
{
    private readonly IRepositoryManager _repositoryManager = repositoryManager;
    private readonly IMapper _mapper = mapper;
    
    public async Task<(IEnumerable<UserDto>,PagedListMetaData)> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var pagedUsers = await _repositoryManager.User.GetUsersAsync(false,request.Parameters);
        var users = _mapper.Map<IEnumerable<UserDto>>(pagedUsers);
        return (users, pagedUsers.MetaData);
    }
}