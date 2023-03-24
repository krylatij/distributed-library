using System;
using System.Collections.Generic;

namespace DistributedLibrary.Data.Entities;

public partial class LoanEntity
{
    public int LoanId { get; set; }

    public int BookId { get; set; }

    public string UserId { get; set; }

    public DateTime DateFrom { get; set; }

    public string? DateTo { get; set; }

    public virtual BookEntity BookEntity { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
