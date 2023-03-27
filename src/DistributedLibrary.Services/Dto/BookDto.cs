using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using DistributedLibrary.Data.Validation;

namespace DistributedLibrary.Services.Dto;

[ExcludeFromCodeCoverage]
public class BookDto
{
    public int BookId { get; set; }

    [Required]
    public string Title { get; set; } = null!;

    [Required]
    public string? Author { get; set; }

    [Required]
    [IsbnValidation]
    public string? Isbn { get; set; }

    public string? Publisher { get; set; }

    public DateTime? PublicationDate { get; set; }

    [Range(1, Int32.MaxValue)]
    public int? PageCount { get; set; }
}