using AutoMapper;
using Repository.Contracts;
using Service.Contracts;

namespace Service;

public class CourierService(IRepositoryManager repositoryManager, IMapper mapper) : ICourierService
{
    private readonly IRepositoryManager _repositoryManager = repositoryManager;
    private readonly IMapper _mapper = mapper;
}