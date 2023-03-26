using System.Diagnostics.CodeAnalysis;

namespace DistributedLibrary.Shared.Configuration;

[ExcludeFromCodeCoverage]
public class ApplicationConfiguration
{
    public const string SectionName = nameof(ApplicationConfiguration);

    public string Host { get; set;}
}