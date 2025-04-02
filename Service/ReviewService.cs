using AutoMapper;
using Repository.Contracts;
using Service.Contracts;

namespace Service;

public class ReviewService(IRepositoryManager repositoryManager, IMapper mapper) : IReviewService
{
    private readonly IRepositoryManager _repositoryManager = repositoryManager;
    private readonly IMapper _mapper = mapper;
}