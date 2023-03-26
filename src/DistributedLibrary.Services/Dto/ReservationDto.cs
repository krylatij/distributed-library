using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedLibrary.Services.Dto;

public class ReservationDto
{
    [Required]
    public int BookId { get; set; }

    [Required]
    public string UserId { get; set; } = null!;

    [Required]

    public DateTime ReservationDate { get; set; }
}