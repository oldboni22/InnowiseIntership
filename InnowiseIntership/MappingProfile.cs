using AutoMapper;
using Domain.Entities;
using Shared.Input;
using Shared.Input.Creation;
using Shared.Input.Update;
using Shared.Output;

namespace InnowiseIntership;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>().ForCtorParam("FullName",options =>
        {
            options.MapFrom(user => $"{user.FirstName} {user.LastName}");
        });
        CreateMap<Courier, CourierDto>();
        CreateMap<Review, ReviewDto>()
            .ForCtorParam("UserName", options =>
            {
                options.MapFrom(review => $"{review.User.FirstName} {review.User.LastName}");
            })
            .ForCtorParam("CourierName", options => 
            {
                options.MapFrom(review => review.Courier.Name );
            });
        CreateMap<Order, OrderDto>();
        
        CreateMap<UserCreationDto, User>();
        CreateMap<CourierCreationDto, Courier>();
        CreateMap<ReviewCreationDto, Review>();
        CreateMap<OrderCreationDto, Order>();
        
        CreateMap<UserForUpdateDto, User>();
        CreateMap<CourierForUpdateDto, Courier>();
        CreateMap<OrderForUpdateDto, Order>();
    }
}