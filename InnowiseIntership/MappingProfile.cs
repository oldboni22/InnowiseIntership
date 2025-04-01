using AutoMapper;
using Domain.Entities;
using Shared.Input;
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
        CreateMap<Review, ReviewDto>();
        CreateMap<Order, OrderDto>();
        
        CreateMap<UserCreationDto, User>();
        CreateMap<CourierCreationDto, Courier>();
        CreateMap<ReviewCreationDto, Review>();
        CreateMap<OrderCreationDto, Order>();
    }
}