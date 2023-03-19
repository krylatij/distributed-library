using DistributedLibrary.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedLibrary.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DistributedLibraryContext _dbContext;

    public UnitOfWork(DistributedLibraryContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<int> CommitAsync()
    {
         
        return await _dbContext.SaveChangesAsync();
    }
}