using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace DistributedLibrary.Data.Repositories;

public class LibraryRepository
{
    private readonly DistributedLibraryContext _dbContext;

    public LibraryRepository(DistributedLibraryContext dbContext)
    {
        _dbContext = dbContext;
    }

    public T Add<T>(T entity) where T : class
    {
        return _dbContext.Set<T>().Add(entity).Entity;
    }

    public T Update<T>(T entity) where T : class
    {
        return _dbContext.Set<T>().Update(entity).Entity;
    }

    public T Delete<T>(T entity) where T : class
    {
        return _dbContext.Set<T>().Remove(entity).Entity;
    }

    public void DeleteMany<T>(IEnumerable<T> entities) where T : class
    {
         _dbContext.Set<T>().RemoveRange(entities);
    }

    public IQueryable<T> GetMany<T>(bool track = false) where T : class
    {
        if (track)
        {
            return _dbContext.Set<T>();
        }

        return _dbContext.Set<T>().AsNoTracking();
    }

    public IQueryable<T> GetMany<T>(Expression<Func<T, bool>> predicate, bool track = false) where T : class
    {
        if (track)
        {
            return _dbContext.Set<T>().Where(predicate);
        }

        return _dbContext.Set<T>().AsNoTracking().Where(predicate);
    }

    public async Task<T?> GetAsync<T>(Expression<Func<T, bool>> predicate, bool track = false) where T : class
    {
        if (track)
        {
            return await _dbContext.Set<T>().SingleOrDefaultAsync(predicate);
        }

        return await _dbContext.Set<T>().AsNoTracking().SingleOrDefaultAsync(predicate);
    }

    public async Task<T?> GetAsync<T, TProperty>(Expression<Func<T, bool>> predicate, 
        Expression<Func<T, TProperty>> include, bool track = false) where T : class
    {
        if (track)
        {
            return await _dbContext.Set<T>().Include(include).SingleOrDefaultAsync(predicate);
        }

        return await _dbContext.Set<T>().AsNoTracking().Include(include).SingleOrDefaultAsync(predicate);
    }

    public async Task<T?> GetAsync<T, TProperty1, TProperty2>(Expression<Func<T, bool>> predicate,
        Expression<Func<T, TProperty1>> include1,
        Expression<Func<T, TProperty2>> include2,
        bool track = false) where T : class
    {
        if (track)
        {
            return await _dbContext.Set<T>().Include(include1).Include(include2).SingleOrDefaultAsync(predicate);
        }

        return await _dbContext.Set<T>().AsNoTracking().Include(include1).Include(include2).SingleOrDefaultAsync(predicate);
    }

}