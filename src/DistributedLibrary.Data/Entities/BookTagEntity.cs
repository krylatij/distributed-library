using System;
using System.Collections.Generic;

namespace DistributedLibrary.Data.Entities;

public partial class BookTagEntity
{
    public int BookTagId { get; set; }

    public int? BookId { get; set; }

    public string Name { get; set; } = null!;

    public virtual BookEntity? Book { get; set; }
}
