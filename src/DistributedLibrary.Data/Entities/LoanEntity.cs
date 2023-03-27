using DistributedLibrary.Data.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace DistributedLibrary.Data.Entities;

[ExcludeFromCodeCoverage]
public partial class LoanEntity : IAuditableEntity
{
    public int LoanId { get; set; }

    public int BookId { get; set; }

    public string UserId { get; set; } = null!;

    public DateTime DateFrom { get; set; }

    public DateTime? DateTo { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual BookEntity Book { get; set; } = null!;

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual User? UpdatedByNavigation { get; set; }

    public virtual User User { get; set; } = null!;

}
