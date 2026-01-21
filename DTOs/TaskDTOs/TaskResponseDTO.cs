using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using go_han.DTOs.Projects;
using go_han.DTOs.User;
using go_han.Models;

namespace go_han.DTOs.TaskDTOs
{
    public class TaskResponseDTO
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }
        public ProjectDetailDto? ProjectData {get; set;}

        public int AssigneeId { get; set; }
        public UserDto? Assignee {get; set;}

        public int? AssignerId { get; set; }
        public UserDto? Assigner {get; set;}

        public required string Title { get; set; }
        public required string Content { get; set; }

        public required string Difficulty { get; set; } // Easy, Medium, Hard

        public DateTime? Deadline { get; set; }

        // Status: 0=Todo, 1=InProgress, 2=MemberApproved, 3=FullyCompleted
        public string Status { get; set; } = string.Empty;

        // Approval System
        public  string MemberComment { get; set; } = string.Empty;
        public int? ApprovedById { get; set; } = null;
        public UserDto? ApprovedBy {get; set;} = null;

        public DateTime? ApprovedAt { get; set; }
    }
}