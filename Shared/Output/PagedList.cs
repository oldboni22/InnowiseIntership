namespace Shared.Output;

public class PagedList<T> : List<T>
{
    public PagedListMetaData MetaData { get; set; }
    public PagedList(List<T> items, int pageNumber, int pageSize)
    {
        MetaData = new PagedListMetaData()
        {
            TotalCount = items.Count,
            PageSize = pageSize,
            CurrentPage = pageNumber,
            PageCount = (int)Math.Ceiling(items.Count / (double)pageSize)
        };
        
        AddRange(items);
    }

    public static PagedList<T> ToPagedList(IEnumerable<T> collection, int pageNumber, int pageSize)
    {
        var items = collection
            .ToList()
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToList();
        
        return new PagedList<T>(items, pageNumber, pageSize);
    }
    
}