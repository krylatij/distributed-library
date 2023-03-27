using AutoFixture;
using DistributedLibrary.Data;
using DistributedLibrary.Data.Entities;
using DistributedLibrary.Data.Repositories;
using DistributedLibrary.Services;
using GridCore;
using Microsoft.EntityFrameworkCore;

namespace DistributedLibrary.UnitTests.Services.Fixtures;

public class GridServiceFixture : IDisposable
{
    public const string TestUser = "me";

    public LibraryRepository LibraryRepository { get; }

    private readonly DistributedLibraryContext _context;

    public GridServiceFixture()
    {
        var options = new DbContextOptionsBuilder<DistributedLibraryContext>().UseInMemoryDatabase("GridService");

        _context = new DistributedLibraryContext(options.Options);

        Populate(_context);

        LibraryRepository = new LibraryRepository(_context);
    }

    private static void Populate(DistributedLibraryContext ctx)
    {
        ctx.Users.Add(new User() {Id = Guid.Empty.ToString()});
        ctx.Users.Add(new User() {Id = TestUser});
            
        var fixture = new Fixture();

        PopulateBooks();
        PopulateLoans();
        PopulateReservations();

        ctx.SaveChanges(true);

        void PopulateBooks()
        {
            var books = new List<BookEntity>()
            {
                new() { ContributorId = TestUser},
                new() { ContributorId = TestUser},
                new() { ContributorId = null},
                new() { ContributorId = null},
                new() { ContributorId = "unknown"},
            };
            books.AddRange(Enumerable.Repeat(new BookEntity { ContributorId = TestUser }, Constants.GridPageSize));

            var bookFixtures = books.Select(x => fixture.Build<BookEntity>()
                .With(e => e.ContributorId, x.ContributorId)
                .With(e => e.CreatedBy, TestUser)
                .Create()).ToArray();

            ctx.Books.AddRange(bookFixtures);
        }

        void PopulateLoans()
        {
            var loans = new List<LoanEntity>()
            {
                new() { UserId = TestUser},
                new() { UserId = TestUser},
                new() { UserId = null},
                new() { UserId = null},
                new() { UserId = "unknown"},
            };

            loans.AddRange(Enumerable.Repeat(new LoanEntity { UserId = TestUser }, Constants.GridPageSize));
               
            var loansFixtures = loans.Select(x => fixture.Build<LoanEntity>()
                .With(e => e.UserId, x.UserId)
                .With(e => e.CreatedBy, TestUser)
                .Create()).ToArray();

            ctx.Loans.AddRange(loansFixtures);
        }

        void PopulateReservations()
        {
            var reservations = new List<ReservationEntity>()
            {
                new() { UserId = TestUser},
                new() { UserId = TestUser},
                new() { UserId = null, CreatedBy = TestUser},
                new() { UserId = null},
                new() { UserId = "unknown", CreatedBy = TestUser},
            };

            reservations.AddRange(Enumerable.Repeat(new ReservationEntity { UserId = TestUser }, Constants.GridPageSize));

            var reservationFixture = reservations.Select(x => fixture.Build<ReservationEntity>()
                .With(e => e.UserId, x.UserId)
                .With(e => e.CreatedBy, TestUser)
                .Create()).ToArray();

            ctx.Reservations.AddRange(reservationFixture);
        }
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}