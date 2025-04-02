namespace Repository.Contracts;

public interface IRepositoryManager
{
    Task SaveAsync();

    ICourierRepository Courier { get; }
    IUserRepository User { get; }
    IOrderRepository Order { get; }
    IReviewRepository Review{ get; }
}