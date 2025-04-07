using AutoMapper;
using Domain.Entities;
using Exceptions.NotFound;
using Repository.Contracts;
using Service.Contracts;
using Shared.Input.Creation;
using Shared.Input.PagingParameters;
using Shared.Output;

namespace Service;

public class ReviewService(IRepositoryManager repositoryManager, IMapper mapper) : IReviewService
{
    private readonly IRepositoryManager _repositoryManager = repositoryManager;
    private readonly IMapper _mapper = mapper;
    
    private async Task<User> TryGetUserByIdAsync(int id,bool trackChanges)
    {
        var user = await _repositoryManager.User.GetUserByIdAsync(id, trackChanges);
        if (user == null)
            throw new UserNotFoundException(id);
        return user;
    }
    private async Task<Courier> TryGetCourierAsync(int id,bool trackChanges)
    {
        var courier = await _repositoryManager.Courier.GetCourierByIdAsync(id,trackChanges);
        if (courier == null)
            throw new CourierFoundException(id);
        
        return courier;
    }

    private async Task<Review> TryGetReviewByIdAsync(int userId, int courierId, int id, bool trackChanges)
    {
        var review = await _repositoryManager.Review.GetReviewByIdAsync(userId, courierId, id,trackChanges);
        if (review == null)
            throw new ReviewFoundException(id);
        return review;
    } 
    
    public async Task<(IEnumerable<ReviewDto> reviews, PagedListMetaData metaData)> GetUserReviewsAsync(int userId, bool trackChanges
    , ReviewRequestParameters parameters)
    {
        var user = await TryGetUserByIdAsync(userId, trackChanges);

        var pagedReviews = await _repositoryManager.Review.GetUserReviewsAsync(userId, trackChanges,parameters);
        var reviews = _mapper.Map<IEnumerable<ReviewDto>>(pagedReviews);

        return (reviews,pagedReviews.MetaData);
    }

    public async Task<(IEnumerable<ReviewDto> reviews, PagedListMetaData metaData)> GetCourierReviewsAsync(int courierId, bool trackChanges
        ,ReviewRequestParameters parameters)
    {
        var courier = await TryGetCourierAsync(courierId, trackChanges);
        
        var pagedReviews = await _repositoryManager.Review.GetCourierReviewsAsync(courierId, trackChanges,parameters);
        var reviews = _mapper.Map<IEnumerable<ReviewDto>>(pagedReviews);

        return (reviews,pagedReviews.MetaData);
    }

    public async Task<ReviewDto> GetReviewByIdAsync(int userId, int courierId, int id, bool trackChanges)
    {
        var user = await TryGetUserByIdAsync(userId,trackChanges);
        var courier = await TryGetCourierAsync(courierId, trackChanges);
        var review = await TryGetReviewByIdAsync(userId, courierId, id, trackChanges);
        
        var result = _mapper.Map<ReviewDto>(review);

        return result;
    }

    public async Task<ReviewDto> CreateReviewAsync(int userId, int courierId, ReviewCreationDto review)
    {
        var user = await TryGetUserByIdAsync(userId,false);
        var courier = await TryGetCourierAsync(courierId, false);

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
        var user = await TryGetUserByIdAsync(userId,false);
        var courier = await TryGetCourierAsync(courierId, false);
        var review = await TryGetReviewByIdAsync(userId, courierId, id, false);
        
        _repositoryManager.Review.DeleteReview(review);
        await _repositoryManager.SaveAsync();
    }
}