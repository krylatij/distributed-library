using System.Diagnostics.CodeAnalysis;
namespace DistributedLibrary.Shared.Configuration;

[ExcludeFromCodeCoverage]
public class CommunicationServiceConfiguration
{
    public const string SectionName = nameof(CommunicationServiceConfiguration);

    public string ConnectionString { get; set; } = default!;

    public string Sender { get; set; } = default!;

    public string Subject { get; set; } = default!;

    public string Message { get; set; } = default!;
}