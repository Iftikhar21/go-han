using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace go_han.DTOs.Projects
{
    public class ProjectDetailDto
    {
        public int Id { get; set; }
        public string ProjectName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string Lead { get; set; } = null!;
        public string? CoLead { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public List<ProjectMemberDto> Members { get; set; } = new();
    }
}