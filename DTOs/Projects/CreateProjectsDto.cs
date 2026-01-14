using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace go_han.DTOs.Projects
{
    public class CreateProjectsDto
    {
        public string ProjectName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int LeadId { get; set; }
        public int? CoLeadId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}