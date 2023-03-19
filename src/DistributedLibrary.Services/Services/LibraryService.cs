using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DistributedLibrary.Data.Entities;
using DistributedLibrary.Data.Repositories;
using DistributedLibrary.Shared.Dto;
using Microsoft.Extensions.Logging;

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

    public async Task<ResponseDto<BookEntity[]>> GetBooksAsync()
    {
        var items = await _libraryRepository.GetManyBooksAsync();

        return new ResponseDto<BookEntity[]>(items);
    }
}