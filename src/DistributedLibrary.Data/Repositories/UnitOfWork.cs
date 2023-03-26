using DistributedLibrary.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DistributedLibrary.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DistributedLibraryContext _dbContext;

    public UnitOfWork(DistributedLibraryContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<int> CommitAsync(string? userId)
    {
        foreach (var added in GetAuditableEntities(EntityState.Added))
        {
            added.CreatedAt = DateTime.Now;
            added.CreatedBy = userId;
            added.UpdatedAt = DateTime.Now;
            added.UpdatedBy = userId;
        }

        foreach (var updated in GetAuditableEntities(EntityState.Modified))
        {
            updated.UpdatedAt = DateTime.Now;
            updated.UpdatedBy = userId;
        }

        var result = await _dbContext.SaveChangesAsync();

        _dbContext.ChangeTracker.Clear();

        return result;

        IEnumerable<IAuditableEntity> GetAuditableEntities(EntityState state){

            return _dbContext.ChangeTracker.Entries()
                .Where(t => t.Entity is IAuditableEntity && t.State == state)
                .Select(t => t.Entity).Cast<IAuditableEntity>();
        }
    }
}