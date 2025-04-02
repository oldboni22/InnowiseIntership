using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public abstract class RepositoryBase<T>(RepositoryContext context) where T : class
{
    private readonly RepositoryContext _context = context;

    protected IQueryable<T> FindAll(bool trackChanges)
    {
        var entities = _context.Set<T>();

        return trackChanges ? entities : entities.AsNoTracking();
    }

    protected IQueryable<T> FindByCondition(Expression<Func<T, bool>> condition, bool trackChanges)
    {
        var entities = _context.Set<T>().Where(condition);
        return trackChanges ? entities : entities.AsNoTracking();
    }

    protected void Create(T entity) => _context.Set<T>().Add(entity);
    protected void Update(T entity) => _context.Set<T>().Update(entity);
    protected void Delete(T entity) => _context.Set<T>().Remove(entity);

}