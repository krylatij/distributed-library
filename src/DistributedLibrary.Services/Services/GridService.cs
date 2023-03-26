using System.Linq;
using System.Security.Cryptography.X509Certificates;
using DistributedLibrary.Data.Entities;
using DistributedLibrary.Data.Repositories;
using DistributedLibrary.Services.Dto;
using GridCore.Server;
using GridShared;
using GridShared.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

namespace DistributedLibrary.Services.Services;

public class GridService
{
    private readonly LibraryRepository _libraryRepository;
   
    public GridService(LibraryRepository libraryRepository)
    {
        _libraryRepository = libraryRepository;
    }

    public async Task<ItemsDTO<BookEntity>> GetBooksAsync(
        Action<IGridColumnCollection<BookEntity>> columns,
        QueryDictionary<StringValues> query,
        string? userId)
    {
        var server = new GridCoreServer<BookEntity>(
                _libraryRepository.GetMany<BookEntity>(x => userId == null || x.ContributorId == userId)
                    .Include(x => x.Holder)
                    .Include(x => x.Contributor)
                    .OrderByDescending(x => x.CreatedAt), query, false, "books", columns)
            .WithPaging(Constants.GridPageSize)
            .Sortable()
            .Filterable(true);

        return await server.GetItemsToDisplayAsync(async x => await x.ToListAsync());

    }

    public async Task<ItemsDTO<LoanEntity>> GetLoansAsync(
        Action<IGridColumnCollection<LoanEntity>> columns,
        QueryDictionary<StringValues> query,
        string? userId)
    {
        var server = new GridCoreServer<LoanEntity>(
                _libraryRepository.GetMany<LoanEntity>(x => userId == null || x.UserId == userId)
                    .Include(x => x.Book)
                    .OrderBy(x => x.DateTo)
                    .ThenByDescending(x => x.DateFrom), query, false, "books", columns)
            .WithPaging(Constants.GridPageSize)
            .Sortable()
            .Filterable(true);

        return await server.GetItemsToDisplayAsync(async x => await x.ToListAsync());
    }

    public async Task<ItemsDTO<ReservationEntity>> GetReservationsAsync(
        Action<IGridColumnCollection<ReservationEntity>> columns,
        QueryDictionary<StringValues> query,
        string? userId)
    {
        var server = new GridCoreServer<ReservationEntity>(
                _libraryRepository.GetMany<ReservationEntity>(x => userId == null || (x.UserId == userId || x.CreatedBy == userId))
                    .Include(x => x.User)
                    .Include(x => x.CreatedByNavigation)
                    .Include(x => x.Book)
                .OrderByDescending(x => x.CreatedAt), query, false, "reservations", columns)
            .WithPaging(Constants.GridPageSize)
            .Sortable()
            .Filterable(true);

        return await server.GetItemsToDisplayAsync(async x => await x.ToListAsync());
    }
}