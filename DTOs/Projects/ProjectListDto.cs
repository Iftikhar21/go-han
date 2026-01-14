using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace go_han.DTOs.Projects
{
    public class ProjectListDto
    {
        public int Id { get; set; }
        public string ProjectName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string LeadName { get; set; }= null!;
        public string? CoLeadName { get; set; }   // optional
        public string Status { get; set; } = null!;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}