using Repository.Contracts;

namespace Repository;

public class RepositoryManager(RepositoryContext context) : IRepositoryManager
{
    private readonly RepositoryContext _context = context;

    private readonly Lazy<IOrderRepository> _order = new Lazy<IOrderRepository>
        (new OrderRepository(context));
    private readonly Lazy<IReviewRepository> _review = new Lazy<IReviewRepository>
        (new ReviewRepository(context));
    private readonly Lazy<IUserRepository> _user = new Lazy<IUserRepository>
        (new UserRepository(context));
    private readonly Lazy<ICourierRepository> _courier = new Lazy<ICourierRepository>
        (new CourierRepository(context));
    
    public async Task SaveAsync() => await _context.SaveChangesAsync();

    public ICourierRepository Courier => _courier.Value;
    public IUserRepository User => _user.Value;
    public IOrderRepository Order => _order.Value;
    public IReviewRepository Review => _review.Value;
}