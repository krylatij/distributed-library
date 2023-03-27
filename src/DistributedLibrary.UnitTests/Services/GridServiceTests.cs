using DistributedLibrary.Data;
using DistributedLibrary.Data.Entities;
using DistributedLibrary.Data.Repositories;
using DistributedLibrary.Services;
using DistributedLibrary.Services.Services;
using DistributedLibrary.UnitTests.Services.Fixtures;
using FakeItEasy;
using GridShared.Sorting;
using GridShared;
using GridShared.Utility;
using Microsoft.Extensions.Primitives;
using MockQueryable.Moq;
using GridCore;
using Microsoft.EntityFrameworkCore;

namespace DistributedLibrary.UnitTests.Services
{
    public class GridServiceTests : IClassFixture<GridServiceFixture>
    {
        private readonly GridServiceFixture _fixture;

        public GridServiceTests(GridServiceFixture fixture)
        {
            _fixture = fixture;
        }

        private static GridService BuildGridService<T>(IEnumerable<T> entities) where T : class
        {
            var options = new DbContextOptionsBuilder<DistributedLibraryContext>().UseInMemoryDatabase("GridService");

            new DistributedLibraryContext(options.Options);

            var mock = entities.AsQueryable().BuildMockDbSet();

            var ctx = A.Fake<DistributedLibraryContext>();

            A.CallTo(() => ctx.Set<T>()).Returns(mock.Object);

            var repo = new LibraryRepository(ctx);

            return new GridService(repo);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(GridServiceFixture.TestUser)]
        [InlineData("no data")]
        public async Task GridService_GetBooksAsync_Ok(string? userId)
        {
            var service = new GridService(_fixture.LibraryRepository);

            Action<IGridColumnCollection<BookEntity>> columns = c =>
            {
                c.Add(o => o.BookId).Titled("Id").Sortable(true, GridSortMode.TwoState);
                c.Add(o => o.Isbn).Filterable(true);
                c.Add(o => o.Author).Sortable(true);
                c.Add(o => o.Contributor.UserName).Titled("Contributor").Sortable(true);
                c.Add(o => o.Holder.UserName).Titled("Holder").Sortable(true);
                c.Add(o => o.CreatedAt).Titled("Created");
            };

            var result = await service.GetBooksAsync(columns, new QueryDictionary<StringValues>(), userId);

            var books = await _fixture.LibraryRepository.GetMany<BookEntity>().ToArrayAsync();
            Assert.NotEmpty(books);

            Assert.NotNull(result);
            Assert.NotNull(result.Pager);

            Assert.True(Constants.GridPageSize > 5); // you have to review test data otherwise

            Assert.Equal(Constants.GridPageSize, result.Pager.PageSize);
            Assert.NotNull(result.Items);
            Assert.Equal(books.Where(x => userId == null || x.ContributorId == userId).Take(Constants.GridPageSize).Count(), result.Items.Count());
        }

        [Theory]
        [InlineData(null)]
        [InlineData(GridServiceFixture.TestUser)]
        [InlineData("no data")]
        public async Task GridService_GetLoansAsync_Ok(string? userId)
        {
            var service = new GridService(_fixture.LibraryRepository);

            Action<IGridColumnCollection<LoanEntity>> columns = c =>
            {
                c.Add(o => o.BookId).Titled("Id").Sortable(true, GridSortMode.TwoState);
                c.Add(o => o.CreatedAt).Titled("Created");
            };

            var result = await service.GetLoansAsync(columns, new QueryDictionary<StringValues>(), userId);

            var loans = await _fixture.LibraryRepository.GetMany<LoanEntity>().ToArrayAsync();
            Assert.NotEmpty(loans);

            Assert.NotNull(result);
            Assert.NotNull(result.Pager);

            Assert.True(Constants.GridPageSize > 5); // you have to review test data otherwise

            Assert.Equal(Constants.GridPageSize, result.Pager.PageSize);
            Assert.NotNull(result.Items);
            Assert.Equal(loans.Where(x => userId == null || x.UserId == userId).Take(Constants.GridPageSize).Count(), result.Items.Count());
        }

        [Theory]
        [InlineData(null)]
        [InlineData(GridServiceFixture.TestUser)]
        [InlineData("no data")]
        public async Task GetReservationsAsyncTest(string? userId)
        {
            var service = new GridService(_fixture.LibraryRepository);

            Action<IGridColumnCollection<LoanEntity>> columns = c =>
            {
                c.Add(o => o.BookId).Titled("Id").Sortable(true, GridSortMode.TwoState);
                c.Add(o => o.CreatedAt).Titled("Created");
            };

            var result = await service.GetLoansAsync(columns, new QueryDictionary<StringValues>(), userId);

            var reservations = await _fixture.LibraryRepository.GetMany<ReservationEntity>().ToArrayAsync();
            Assert.NotEmpty(reservations);

            Assert.NotNull(result);
            Assert.NotNull(result.Pager);

            Assert.True(Constants.GridPageSize > 5); // you have to review test data otherwise

            Assert.Equal(Constants.GridPageSize, result.Pager.PageSize);
            Assert.NotNull(result.Items);
            Assert.Equal(reservations.Where(x => userId == null || (x.UserId == userId || x.CreatedBy == userId)).Take(Constants.GridPageSize).Count(), result.Items.Count());
        }
    }
}