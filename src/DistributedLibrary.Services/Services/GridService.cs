using DistributedLibrary.Data.Entities;
using DistributedLibrary.Data.Interfaces;
using DistributedLibrary.Data.Repositories;
using GridCore.Server;
using GridShared;
using GridShared.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

namespace DistributedLibrary.Services.Services;

public class GridService
{
    private readonly ILibraryRepository _libraryRepository;
   
    public GridService(ILibraryRepository libraryRepository)
    {
        _libraryRepository = libraryRepository;
    }

    internal virtual IGridServer<T> GetServer<T>(IEnumerable<T> entities, 
        QueryDictionary<StringValues> query, 
        bool renderOnlyRows,
        string gridName,
        Action<IGridColumnCollection<T>> columns,
        int pageSize)
    {
        return new GridCoreServer<T>(entities, query, renderOnlyRows, gridName, columns)
            .WithPaging(pageSize)
            .Sortable()
            .Filterable(true);
    }

    public async Task<ItemsDTO<BookEntity>> GetBooksAsync(
        Action<IGridColumnCollection<BookEntity>> columns,
        QueryDictionary<StringValues> query,
        string? userId)
    {
        var server = GetServer(_libraryRepository.GetMany<BookEntity>(x => userId == null || x.ContributorId == userId)
                                .Include(x => x.Holder)
                                .Include(x => x.Contributor)
                                .OrderByDescending(x => x.CreatedAt),
            query, false, "books", columns, Constants.GridPageSize);

        return await server.GetItemsToDisplayAsync(async x => await x.ToListAsync());

    }

    public async Task<ItemsDTO<LoanEntity>> GetLoansAsync(
        Action<IGridColumnCollection<LoanEntity>> columns,
        QueryDictionary<StringValues> query,
        string? userId)
    {
        var server = GetServer(
            _libraryRepository.GetMany<LoanEntity>(x => userId == null || x.UserId == userId)
                .Include(x => x.Book)
                .OrderBy(x => x.DateTo)
                .ThenByDescending(x => x.DateFrom), query, false, "loans", columns, Constants.GridPageSize);

        return await server.GetItemsToDisplayAsync(async x => await x.ToListAsync());
    }

    public async Task<ItemsDTO<ReservationEntity>> GetReservationsAsync(
        Action<IGridColumnCollection<ReservationEntity>> columns,
        QueryDictionary<StringValues> query,
        string? userId)
    {
        var server = GetServer(
            _libraryRepository
                .GetMany<ReservationEntity>(x => userId == null || (x.UserId == userId || x.CreatedBy == userId))
                .Include(x => x.User)
                .Include(x => x.CreatedByNavigation)
                .Include(x => x.Book)
                .OrderByDescending(x => x.CreatedAt), query, false, "reservations", columns, Constants.GridPageSize);

        return await server.GetItemsToDisplayAsync(async x => await x.ToListAsync());
    }
}