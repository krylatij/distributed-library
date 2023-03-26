using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedLibrary.Services.Dto;

public class LoanDto
{
    public int LoanId { get; set; }

    public string BookId { get; set; }

    public string UserId { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
}