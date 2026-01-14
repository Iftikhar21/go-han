using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using go_han.Models;

namespace go_han.DTOs.TaskDTOs
{
    public class TaskResponseDTO
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }
        public Project? ProjectData {get; set;}

        public int AssigneeId { get; set; }
        public User? Assignee {get; set;}

        public required string Title { get; set; }
        public required string Content { get; set; }

        public required string Difficulty { get; set; } // Easy, Medium, Hard

        public DateTime? Deadline { get; set; }

        // Status: 0=Todo, 1=InProgress, 2=MemberApproved, 3=FullyCompleted
        public int Status { get; set; } = 0;


        // Approval System
        public  string MemberComment { get; set; } = string.Empty;
        public int? ApprovedById { get; set; }
        public User? ApprovedBy {get; set;}

        public DateTime? ApprovedAt { get; set; }
    }
}