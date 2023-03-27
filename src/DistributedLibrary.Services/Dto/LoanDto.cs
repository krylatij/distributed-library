using System.Diagnostics.CodeAnalysis;

namespace DistributedLibrary.Services.Dto;

[ExcludeFromCodeCoverage]
public class LoanDto
{
    public int LoanId { get; set; }

    public string BookId { get; set; }

    public string UserId { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
}