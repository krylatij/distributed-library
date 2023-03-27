using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DistributedLibrary.Services.Mapping;

namespace DistributedLibrary.UnitTests.Services.Fixtures;

public class LibraryServiceFixture
{
    public IMapper Mapper { get;  }
    public LibraryServiceFixture()
    {
        var config = new MapperConfiguration(cfg => {
            cfg.AddProfile<AutomapperProfile>();
        });

        Mapper = config.CreateMapper();
    }
}