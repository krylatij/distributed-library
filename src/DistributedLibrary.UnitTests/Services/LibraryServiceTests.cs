using Castle.Core.Logging;
using DistributedLibrary.Data.Entities;
using DistributedLibrary.Data.Repositories;
using DistributedLibrary.Services.Dto;
using DistributedLibrary.Services.Services;
using DistributedLibrary.Shared.Configuration;
using DistributedLibrary.UnitTests.Services.Fixtures;
using FakeItEasy;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;
using DistributedLibrary.Shared.Enums;
using DistributedLibrary.UnitTests.Extensions;
using DistributedLibrary.Data.Interfaces;

namespace DistributedLibrary.UnitTests.Services;

public class LibraryServiceTests : IClassFixture<LibraryServiceFixture>
{
    private readonly LibraryServiceFixture _fixture;

    public LibraryServiceTests(LibraryServiceFixture fixture)
    {
        _fixture = fixture;
    }

    private LibraryService GetService(ILibraryRepository repository)
    {
        return new LibraryService(A.Fake<ILogger<LibraryService>>(),
            repository,
            A.Fake<NotificationService>(), 
            Options.Create(new ApplicationConfiguration()),
            A.Fake<UnitOfWork>(),
            _fixture.Mapper);
    }

    [Fact]
    public async Task LibraryService_UpsertBookAsync_AlreadyExists()
    {
        var book = new BookDto { Isbn = "123" };

        var repo = A.Fake<ILibraryRepository>();

        A.CallTo(() => repo.GetMany(A<Expression<Func<BookEntity, bool>>>._, false))
            .Returns(new BookEntity
            {
                BookId = 1,
                Isbn = book.Isbn
            }.ToQueryableAsync());

        var service = GetService(repo);

        var response = await service.UpsertBookAsync(book, "");

        Assert.NotNull(response);
        Assert.Equal(ResponseState.ValidationFailed, response.ResponseState);
    }

    [Fact]
    public async Task LibraryService_GetBookAsync_OkExists()
    {
        var book = new BookEntity
        {
            BookId = 123,
        };

        var repo = A.Fake<ILibraryRepository>();

        A.CallTo(() => repo.GetAsync(A<Expression<Func<BookEntity?, bool>>>._, false))
            .Returns(Task.FromResult(book));

        var service = GetService(repo);

        var response = await service.GetBookAsync(book.BookId);

        Assert.NotNull(response);
        Assert.NotNull(response.Result);
        Assert.Equal(book.BookId, response.Result.BookId);
        Assert.Equal(ResponseState.Ok, response.ResponseState);
    }

    [Fact]
    public async Task LibraryService_GetBookAsync_OkNull()
    {
        const int bookId = 123;

        var repo = A.Fake<ILibraryRepository>();

        A.CallTo(() => repo.GetAsync(A<Expression<Func<BookEntity?, bool>>>._, false))
            .Returns(Task.FromResult(default(BookEntity?)));

        var service = GetService(repo);

        var response = await service.GetBookAsync(bookId);

        Assert.NotNull(response);
        Assert.Null(response.Result);
        Assert.Equal(ResponseState.Ok, response.ResponseState);
    }


    [Theory]
    [InlineData(null)]
    [InlineData("me")]
    public async Task LibraryService_GetBooksAsync_Ok(string? userId)
    {
        var book = new BookEntity
        {
            BookId = 123
        };

        var repo = A.Fake<ILibraryRepository>();

        A.CallTo(() => repo.GetMany(A<Expression<Func<BookEntity, bool>>>._, false))
            .Returns(book.ToQueryableAsync());

        var service = GetService(repo);

        var response = await service.GetBooksAsync(userId);

        Assert.NotNull(response);
        Assert.Single(response);
    }

    [Fact]
    public async Task LibraryService_GetUsersAsync_Ok()
    {
        var user = new User
        {
            Id = "123"
        };
        
        var repo = A.Fake<ILibraryRepository>();

        A.CallTo(() => repo.GetMany<User>(false))
            .Returns(user.ToQueryableAsync());
        
        var service = GetService(repo);

        var response = await service.GetUsersAsync();

        Assert.NotNull(response);
        Assert.Single(response);
    }

    [Fact]
    public async Task LibraryService_GetUserAsync_Ok()
    {
        var user = new User
        {
            Id = "123"
        };

        var repo = A.Fake<ILibraryRepository>();

        A.CallTo(() => repo.GetAsync<User>(A<Expression<Func<User, bool>>>._, false))
            .Returns(Task.FromResult(user));

        var service = GetService(repo);

        var response = await service.GetUserAsync(user.Id);

        Assert.NotNull(response);
        Assert.Equal(user.Id, response.Id);
    }
}