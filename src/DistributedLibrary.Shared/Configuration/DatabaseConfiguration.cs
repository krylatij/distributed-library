namespace DistributedLibrary.Shared.Configuration;

public class DatabaseConfiguration
{
    public const string SectionName = nameof(DatabaseConfiguration);

    public string ConnectionString { get; set; } = default!;
}