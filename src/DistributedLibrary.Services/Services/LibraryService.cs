using AutoMapper;
using DistributedLibrary.Data.Entities;
using DistributedLibrary.Data.Repositories;
using DistributedLibrary.Services.Dto;
using DistributedLibrary.Shared.Dto;
using DistributedLibrary.Shared.Enums;
using GridCore.Server;
using GridShared;
using GridShared.Utility;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace DistributedLibrary.Services.Services;

public class LibraryService
{
    private readonly LibraryRepository _libraryRepository;
    private readonly IMapper _mapper;

    public LibraryService(ILogger<LibraryService> logger, 
        LibraryRepository libraryRepository,
        IMapper mapper)
    {
        _libraryRepository = libraryRepository;
        _mapper = mapper;
    }

    public async Task<ItemsDTO<BookEntity>> GetBooksAsync(Action<IGridColumnCollection<BookEntity>> columns,
        QueryDictionary<StringValues> query)
    {
        var server = new GridCoreServer<BookEntity>(await _libraryRepository.GetManyBooksAsync(), query, false, "books", columns)
            .WithPaging(10)
            .Sortable()
            .Filterable(true);

        return await server.GetItemsToDisplayAsync(async x => await Task.FromResult(x.ToList()));

    }

    public async Task<ResponseDto<BookDto>> UpsertBookAsync(BookDto bookDto, string userId)
    {
        var entity = _mapper.Map<BookEntity>(bookDto);
        entity.ContributorId = userId;

        if (!string.IsNullOrEmpty(bookDto.Isbn))
        {
            var oldBook = await _libraryRepository.GetBookAsync(x => x.Isbn == bookDto.Isbn && x.BookId != bookDto.BookId);

            if (oldBook != null)
            {
                return new ResponseDto<BookDto>(null, ResponseState.ValidationFailed, $"Book with ISBN {bookDto.Isbn} already exists.");
            }
        }

        if (bookDto.BookId != default)
        {
            entity = await _libraryRepository.UpdateBookAsync(entity);
        }
        else
        {
            entity = await _libraryRepository.AddBookAsync(entity);
        }

        var dto = _mapper.Map<BookDto>(entity);

        return new ResponseDto<BookDto>(dto, ResponseState.Ok);
    }

    public async Task<ResponseDto<BookDto>> GetBookAsync(int bookId)
    {
        var entity = await _libraryRepository.GetBookAsync(bookId);

        var dto = _mapper.Map<BookDto>(entity);

        return new ResponseDto<BookDto>(dto, ResponseState.Ok);
    }

    public async Task DeleteBookAsync(int bookId)
    {
        await _libraryRepository.DeleteBookAsync(bookId);
    }
}