using System.Linq.Expressions;

namespace DistributedLibrary.Data.Interfaces;

public interface ILibraryRepository : IDisposable
{
    T Add<T>(T entity) where T : class;
    T Update<T>(T entity) where T : class;
    T Delete<T>(T entity) where T : class;
    void DeleteMany<T>(IEnumerable<T> entities) where T : class;
    IQueryable<T> GetMany<T>(bool track = false) where T : class;
    IQueryable<T> GetMany<T>(Expression<Func<T, bool>> predicate, bool track = false) where T : class;
    Task<T?> GetAsync<T>(Expression<Func<T, bool>> predicate, bool track = false) where T : class;

    Task<T?> GetAsync<T, TProperty>(Expression<Func<T, bool>> predicate,
        Expression<Func<T, TProperty>> include, bool track = false) where T : class;

    Task<T?> GetAsync<T, TProperty1, TProperty2>(Expression<Func<T, bool>> predicate,
        Expression<Func<T, TProperty1>> include1,
        Expression<Func<T, TProperty2>> include2,
        bool track = false) where T : class;
}