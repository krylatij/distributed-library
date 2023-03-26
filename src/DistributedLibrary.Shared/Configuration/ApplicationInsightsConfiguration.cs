using System.Diagnostics.CodeAnalysis;

namespace DistributedLibrary.Shared.Configuration;

[ExcludeFromCodeCoverage]
public class ApplicationInsightsConfiguration
{
    public const string SectionName = nameof(ApplicationInsightsConfiguration);

    public string ConnectionString { get; set;}
}