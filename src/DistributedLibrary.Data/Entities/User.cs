using System;
using System.Collections.Generic;

namespace DistributedLibrary.Data.Entities;

public partial class User
{
    public Guid UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string? City { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<BookEntity> BookContributors { get; } = new List<BookEntity>();

    public virtual ICollection<BookEntity> BookHolders { get; } = new List<BookEntity>();

    public virtual ICollection<LoanEntity> Loans { get; } = new List<LoanEntity>();
}
