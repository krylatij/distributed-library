using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedLibrary.Services.Dto;

public class BookDto
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public string? Author { get; set; }

    public string? Isbn { get; set; }

    public string? Publisher { get; set; }

    public DateTime? PublicationDate { get; set; }

    public int? PageCount { get; set; }
}