using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace go_han.DTOs.Division
{
    public class DivisionDTO
    {
        public int Id { get; set; }
        public required string DivisionName { get; set; } = string.Empty;
    }
}