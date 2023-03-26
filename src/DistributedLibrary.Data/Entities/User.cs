using Microsoft.AspNetCore.Identity;

namespace DistributedLibrary.Data.Entities;

public partial class User : IdentityUser
{
    public virtual ICollection<BookEntity> BookContributors { get; } = new List<BookEntity>();

    public virtual ICollection<BookEntity> BookCreatedByNavigations { get; } = new List<BookEntity>();

    public virtual ICollection<BookEntity> BookHolders { get; } = new List<BookEntity>();

    public virtual ICollection<BookEntity> BookUpdatedByNavigations { get; } = new List<BookEntity>();

    public virtual ICollection<LoanEntity> LoanCreatedByNavigations { get; } = new List<LoanEntity>();

    public virtual ICollection<LoanEntity> LoanUpdatedByNavigations { get; } = new List<LoanEntity>();

    public virtual ICollection<LoanEntity> LoanUsers { get; } = new List<LoanEntity>();

    public virtual ICollection<ReservationEntity> ReservationCreatedByNavigations { get; } = new List<ReservationEntity>();

    public virtual ICollection<ReservationEntity> ReservationUpdatedByNavigations { get; } = new List<ReservationEntity>();

    public virtual ICollection<ReservationEntity> ReservationUsers { get; } = new List<ReservationEntity>();



}
