using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
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

    public async Task<IQueryable<BookEntity>> GetManyBooksAsync()
    {
        return  _dbContext.Books.AsNoTracking().Include(x => x.Contributor);
    }


    public async Task<BookEntity> AddBookAsync(BookEntity bookEntity)
    {
        await _dbContext.Books.AddAsync(bookEntity);

        await _unitOfWork.CommitAsync();

        return bookEntity;
    }

    public async Task<BookEntity> UpdateBookAsync(BookEntity bookEntity)
    {
        _dbContext.Books.Entry(bookEntity).State = EntityState.Modified;

        await _unitOfWork.CommitAsync();

        return bookEntity;
    }

    public async Task<BookEntity?> GetBookAsync(int bookId)
    {
        return await GetBookAsync(x => x.BookId == bookId);
    }

    public async Task<BookEntity?> GetBookAsync(Expression<Func<BookEntity, bool>> predicate)
    {
        return await _dbContext.Books.AsNoTracking().FirstOrDefaultAsync(predicate);
    }

    public async Task DeleteBookAsync(int bookId)
    {
        var book = await _dbContext.Books.FirstOrDefaultAsync(x => x.BookId == bookId);
        if (book == null)
        {
            return;
        }

        _dbContext.BookTags.RemoveRange(_dbContext.BookTags.Where(x => x.BookId == bookId));
        _dbContext.Loans.RemoveRange(_dbContext.Loans.Where(x => x.BookId == bookId));
        _dbContext.Books.Remove(book);

        await _unitOfWork.CommitAsync();
    }
}