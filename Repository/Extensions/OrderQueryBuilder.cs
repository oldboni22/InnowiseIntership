using System.Reflection;
using System.Text;

namespace Repository.Extensions;

public static class OrderQueryBuilder
{
    public static string CreateQuery<T>(string queryString)
    {
        var queryParams = queryString.Trim().Split(',');
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        
        var builder = new StringBuilder();

        foreach (var param in queryParams)
        {
            if(string.IsNullOrWhiteSpace(param))
                continue;

            var property = param.Split(" ")[0];
            var objectProperty = properties.FirstOrDefault(p 
                => p.Name.Equals(property, StringComparison.InvariantCultureIgnoreCase));
            
            if(objectProperty == null)
                continue;
            
            var direction = param.EndsWith(" desc") ? "descending" : "ascending";
            builder.Append($"{objectProperty.Name} {direction},");
        }
        
        return builder.ToString().TrimEnd(',',' ');
    }
}