using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DistributedLibrary.Data.Entities;
using DistributedLibrary.Services.Dto;

namespace DistributedLibrary.Services.Mapping
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<BookDto, BookEntity>().ReverseMap();
        }
    }
}
