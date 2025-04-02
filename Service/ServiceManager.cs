using AutoMapper;
using Repository.Contracts;
using Service.Contracts;

namespace Service;

public class ServiceManager(IRepositoryManager repositoryManager,IMapper mapper) : IServiceManager
{

    private readonly Lazy<IReviewService> _review = new Lazy<IReviewService>
        (new ReviewService(repositoryManager, mapper));
    private readonly Lazy<IUserService> _user = new Lazy<IUserService>
        (new UserService(repositoryManager, mapper));
    private readonly Lazy<IOrderService> _order = new Lazy<IOrderService>
        (new OrderService(repositoryManager,mapper));
    private readonly Lazy<ICourierService> _courier = new Lazy<ICourierService>
        (new CourierService(repositoryManager, mapper));

    public IReviewService Review => _review.Value;
    public IUserService User => _user.Value;
    public IOrderService Order => _order.Value;
    public ICourierService Courier => _courier.Value;
}