using DistributedLibrary.Data.Interfaces;

namespace DistributedLibrary.Data.Entities;

public partial class BookEntity : IAuditableEntity
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public string? Author { get; set; }

    public string? Isbn { get; set; }

    public string? Genres { get; set; }

    public string? Tags { get; set; }

    public string? Publisher { get; set; }

    public DateTime? PublicationDate { get; set; }

    public int? PageCount { get; set; }

    public string? ContributorId { get; set; }

    public string? HolderId { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }
    
    public virtual User? Contributor { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual User? Holder { get; set; }

    public virtual ICollection<LoanEntity> Loans { get; } = new List<LoanEntity>();

    public virtual ICollection<ReservationEntity> Reservations { get; } = new List<ReservationEntity>();

    public virtual User? UpdatedByNavigation { get; set; }

}
