using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace go_han.Models
{
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }

        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; } = null!;

        public int AssigneeId { get; set; }
        [ForeignKey("AssigneeId")]
        public virtual User Assignee { get; set; } = null!;

        public int AssignerId { get; set; }
        [ForeignKey("AssignerId")]
        public virtual User Assigner { get; set; } = null!;

        [Required, StringLength(200)]
        public required string Title { get; set; }
        public required string Content { get; set; }

        [Required]
        public required string Difficulty { get; set; } // Easy, Medium, Hard

        public DateTime? Deadline { get; set; }

        // Status: 0=Todo, 1=InProgress, 2=MemberApproved, 3=FullyCompleted
        public int Status { get; set; } = 0;

        // Approval System
        public  string MemberComment { get; set; } = string.Empty;

        public int? ApprovedById { get; set; }
        [ForeignKey("ApprovedById")]
        public virtual User ApprovedBy { get; set; } = null!;

        public DateTime? ApprovedAt { get; set; }
    }
}