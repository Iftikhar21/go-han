using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace go_han.DTOs.Division
{
    public class CreateRequestDivisionDTO
    {
        public required string DivisionName { get; set; } = string.Empty;
    }
}