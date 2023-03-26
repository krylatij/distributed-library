using System.Diagnostics.CodeAnalysis;

namespace DistributedLibrary.Shared.Configuration;

[ExcludeFromCodeCoverage]
public class DatabaseConfiguration
{
    public const string SectionName = nameof(DatabaseConfiguration);

    public string ConnectionString { get; set; } = default!;
}