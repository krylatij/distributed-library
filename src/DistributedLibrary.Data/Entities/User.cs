using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DistributedLibrary.Data.Entities;

public partial class User : IdentityUser
{
    public virtual ICollection<BookEntity> BookContributors { get; } = new List<BookEntity>();

    public virtual ICollection<BookEntity> BookHolders { get; } = new List<BookEntity>();

    public virtual ICollection<LoanEntity> Loans { get; } = new List<LoanEntity>();
}
