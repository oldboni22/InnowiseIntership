namespace Shared.Input.Request;

public abstract class RequestParameters
{
    private const int MaxPageSize = 35;
    public int PageNumber { get; set; } = 1;

    private int _pageSize = 25;
    public int PageSize 
    { 
        get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }
}