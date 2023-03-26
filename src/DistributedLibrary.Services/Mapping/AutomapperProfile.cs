using AutoMapper;
using DistributedLibrary.Data.Entities;
using DistributedLibrary.Services.Dto;

namespace DistributedLibrary.Services.Mapping;

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