using System.Diagnostics.CodeAnalysis;
using DistributedLibrary.Data.Interfaces;

namespace DistributedLibrary.Data.Entities;

[ExcludeFromCodeCoverage]
public class ReservationEntity : IAuditableEntity
{
    public int ReservationId { get; set; }

    public int BookId { get; set; }

    public string UserId { get; set; } = null!;

    public DateTime ReservationDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual BookEntity Book { get; set; } = null!;

    public virtual User? CreatedByNavigation { get; set; }

    public virtual User? UpdatedByNavigation { get; set; }

    public virtual User User { get; set; } = null!;

}