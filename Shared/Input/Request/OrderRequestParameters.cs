using Shared.Output;

namespace Shared.Input.Request;

public class OrderRequestParameters : RequestParameters
{
    public OrderStatus OrderStatus { get; set; } =  OrderStatus.Pending;
}