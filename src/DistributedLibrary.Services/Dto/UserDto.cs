using System.Diagnostics.CodeAnalysis;

namespace DistributedLibrary.Services.Dto;

[ExcludeFromCodeCoverage]
public class UserDto
{
    public string Id { get; set; }

    public string Email { get; set; }

    public string UserName { get; set; }

}