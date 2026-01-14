using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace go_han.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(200)]
        public required string ProjectName { get; set; }
        public required string Description { get; set; }

        // Lead Project
        public int LeadId { get; set; }
        [ForeignKey("LeadId")]
        public virtual User Lead { get; set; } = null!;

        // Tangan Kanan (Co-Lead)
        public int? CoLeadId { get; set; }
        [ForeignKey("CoLeadId")]
        public virtual User CoLead { get; set; } = null!;

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; } = "Active";

        public virtual ICollection<ProjectMember> Members { get; set; } = null!;
        public virtual ICollection<TaskItem> Tasks { get; set; } = null!;
    }
}