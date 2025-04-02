using AutoMapper;
using Repository.Contracts;
using Service.Contracts;

namespace Service;

public class OrderService(IRepositoryManager repositoryManager, IMapper mapper) : IOrderService
{
    private readonly IRepositoryManager _repositoryManager = repositoryManager;
    private readonly IMapper _mapper = mapper;
}