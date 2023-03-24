namespace DistributedLibrary.Shared.Configuration;

public class ApplicationInsightsConfiguration
{
    public const string SectionName = nameof(ApplicationInsightsConfiguration);

    public string ConnectionString { get; set;}
}