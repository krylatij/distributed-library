using System;
using System.Collections.Generic;

namespace DistributedLibrary.Data.Entities;

public partial class BookEntity
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public string? Author { get; set; }

    public string? Isbn { get; set; }

    public string? Publisher { get; set; }

    public DateTime? PublicationDate { get; set; }

    public int? PageCount { get; set; }

    public string? ContributorId { get; set; }

    public string? HolderId { get; set; }

    public virtual ICollection<BookTagEntity> BookTags { get; } = new List<BookTagEntity>();

    public virtual User? Contributor { get; set; }

    public virtual User? Holder { get; set; }

    public virtual ICollection<LoanEntity> Loans { get; } = new List<LoanEntity>();
}
