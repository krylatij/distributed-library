using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace DistributedLibrary.Services.Dto;

[ExcludeFromCodeCoverage]
public class ReservationDto
{
    [Required]
    public int BookId { get; set; }

    [Required]
    public string UserId { get; set; } = null!;

    [Required]

    public DateTime ReservationDate { get; set; }
}