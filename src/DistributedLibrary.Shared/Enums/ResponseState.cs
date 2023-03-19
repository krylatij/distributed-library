using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedLibrary.Shared.Enums;

public enum ResponseState
{
    Success = 0,
    AlreadyExist = 1,
    Created = 2,
    NotFound = 3,
    ValidationFailed = 4,
    Error = 5

}