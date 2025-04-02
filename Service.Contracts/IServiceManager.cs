namespace Service.Contracts;

public interface IServiceManager
{
    IReviewService Review { get; }
    IUserService User { get; }
    IOrderService Order { get; }
    ICourierService Courier { get; }
}