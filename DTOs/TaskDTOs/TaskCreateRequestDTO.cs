using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace go_han.DTOs.TaskDTOs
{
    public class TaskCreateRequestDTO
    {
        public int ProjectId { get; set; }

        public int AssigneeId { get; set; }

        public required string Title { get; set; }
        public required string Content { get; set; }

        public required string Difficulty { get; set; } // Easy, Medium, Hard

        public DateTime? Deadline { get; set; }

        // Status: 0=Todo, 1=InProgress, 2=MemberApproved, 3=FullyCompleted
        public int Status { get; set; } = 0;
    }
}