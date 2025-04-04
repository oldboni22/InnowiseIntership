using AutoMapper;
using Domain.Entities;
using Exceptions.NotFound;
using Repository.Contracts;
using Service.Contracts;
using Shared.Input.Creation;
using Shared.Output;

namespace Service;

public class ReviewService(IRepositoryManager repositoryManager, IMapper mapper) : IReviewService
{
    private readonly IRepositoryManager _repositoryManager = repositoryManager;
    private readonly IMapper _mapper = mapper;
    public async Task<IEnumerable<ReviewDto>> GetUserReviewsAsync(int userId, bool trackChanges)
    {
        var user = await _repositoryManager.User.GetUserByIdAsync(userId, false);
        if (user == null)
            throw new UserNotFoundException(userId);

        var reviews = _repositoryManager.Review.GetUserReviewsAsync(userId, trackChanges);
        var result = _mapper.Map<IEnumerable<ReviewDto>>(reviews);

        return result;
    }

    public async Task<IEnumerable<ReviewDto>> GetCourierReviewsAsync(int courierId, bool trackChanges)
    {
        var courier = await _repositoryManager.Courier.GetCourierByIdAsync(courierId, false);
        if (courier == null)
            throw new CourierFoundException(courierId);
        
        var reviews = _repositoryManager.Review.GetCourierReviewsAsync(courierId, trackChanges);
        var result = _mapper.Map<IEnumerable<ReviewDto>>(reviews);

        return result;
    }

    public async Task<ReviewDto> GetReviewByIdAsync(int userId, int courierId, int id, bool trackChanges)
    {
        var user = await _repositoryManager.User.GetUserByIdAsync(userId, false);
        if (user == null)
            throw new UserNotFoundException(userId);
        
        var courier = await _repositoryManager.Courier.GetCourierByIdAsync(courierId, false);
        if (courier == null)
            throw new CourierFoundException(courierId);

        var review = await _repositoryManager.Review.GetReviewByIdAsync(userId, courierId, id,trackChanges);
        if (review == null)
            throw new ReviewFoundException(id);
        
        var result = _mapper.Map<ReviewDto>(review);

        return result;
    }

    public async Task<ReviewDto> CreateReviewAsync(int userId, int courierId, ReviewCreationDto review)
    {
        var user = await _repositoryManager.User.GetUserByIdAsync(userId, false);
        if (user == null)
            throw new UserNotFoundException(userId);
        
        var courier = await _repositoryManager.Courier.GetCourierByIdAsync(courierId, false);
        if (courier == null)
            throw new CourierFoundException(courierId);

        var entity = _mapper.Map<Review>(review);
        entity = entity with
        {
            UserId = userId,
            CourierId = courierId,
            PostedAt = DateTime.UtcNow
        };
        
        _repositoryManager.Review.CreateReview(entity);
        await _repositoryManager.SaveAsync();

        return _mapper.Map<ReviewDto>(entity);
    }

    public async Task DeleteReviewAsync(int userId, int courierId, int id)
    {
        var user = await _repositoryManager.User.GetUserByIdAsync(userId, false);
        if (user == null)
            throw new UserNotFoundException(userId);
        
        var courier = await _repositoryManager.Courier.GetCourierByIdAsync(courierId, false);
        if (courier == null)
            throw new CourierFoundException(courierId);

        var review = await _repositoryManager.Review.GetReviewByIdAsync(userId, courierId, id,false);
        if (review == null)
            throw new ReviewFoundException(id);
        
        _repositoryManager.Review.DeleteReview(review);
        await _repositoryManager.SaveAsync();
    }
}