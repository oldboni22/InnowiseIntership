using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public abstract class RepositoryBase<T>(RepositoryContext context) where T : class
{
    private readonly RepositoryContext _context = context;

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition, bool trackChanges)
    {
        var entities = _context.Set<T>().Where(condition);
        return trackChanges ? entities : entities.AsNoTracking();
    }
    public void Create(T entity) => _context.Set<T>().Add(entity);
    public void Update(T entity) => _context.Set<T>().Update(entity);
    public void Delete(T entity) => _context.Set<T>().Remove(entity);

}