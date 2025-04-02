using AutoMapper;
using Repository.Contracts;
using Service.Contracts;

namespace Service;

public class UserService(IRepositoryManager repositoryManager, IMapper mapper) : IUserService
{
    private readonly IRepositoryManager _repositoryManager = repositoryManager;
    private readonly IMapper _mapper = mapper;
}