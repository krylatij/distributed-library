using AutoMapper;
using DistributedLibrary.Data.Entities;
using DistributedLibrary.Services.Dto;
using System.Diagnostics.CodeAnalysis;

namespace DistributedLibrary.Services.Mapping;

[ExcludeFromCodeCoverage]
public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<BookDto, BookEntity>().ReverseMap();
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<LoanDto, LoanEntity>().ReverseMap();
        CreateMap<ReservationDto, ReservationEntity>().ReverseMap();
    }
}