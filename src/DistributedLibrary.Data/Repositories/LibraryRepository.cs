using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DistributedLibrary.Data.Entities;
using DistributedLibrary.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DistributedLibrary.Data.Repositories;

public class LibraryRepository
{
    private readonly DistributedLibraryContext _dbContext;
    private readonly IUnitOfWork _unitOfWork;

    public LibraryRepository(DistributedLibraryContext dbContext, IUnitOfWork unitOfWork)
    {
        _dbContext = dbContext;
        _unitOfWork = unitOfWork;
    }

    public async Task<BookEntity[]> GetManyBooksAsync()
    {
        return await _dbContext.Books.ToArrayAsync();
    }


    public async Task<BookEntity> AddBookAsync(BookEntity bookEntity)
    {
        await _dbContext.Books.AddAsync(bookEntity);

        await _unitOfWork.CommitAsync();

        return bookEntity;
    }
}