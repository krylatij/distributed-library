using System.Diagnostics.CodeAnalysis;

namespace DistributedLibrary.UI.Auth;

[ExcludeFromCodeCoverage]
public class TokenProvider
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }

    public string? XsrfToken { get; set; }
}