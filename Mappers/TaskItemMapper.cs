using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using go_han.DTOs.TaskDTOs;
using go_han.Models;

namespace go_han.Mappers
{
    public class TaskItemMapper
    {
        public static TaskResponseDTO TaskResponse(TaskItem task)
        {
            return new TaskResponseDTO
            {
                Id = task.Id,
                ProjectId = task.ProjectId,
                ProjectData = task.Project,
                AssigneeId = task.AssigneeId,
                Assignee = task.Assignee,
                Title = task.Title,
                Content = task.Content,
                Difficulty = task.Difficulty,
                Deadline = task.Deadline,
                Status = task.Status,

                // Approval
                MemberComment = task.MemberComment,
                ApprovedById = task.ApprovedById,
                ApprovedBy = task.ApprovedBy,
                ApprovedAt = task.ApprovedAt
            };
        }

        public static TaskItem TaskCreate(TaskCreateRequestDTO dto)
        {
            return new TaskItem
            {
                ProjectId = dto.ProjectId,
                AssigneeId = dto.AssigneeId,
                Title = dto.Title,
                Content = dto.Content,
                Difficulty = dto.Difficulty,
                Deadline = dto.Deadline,
                Status = dto.Status
            };
        }

        // public static TaskItem TaskRequest(TaskRequestDTO dto)
        // {
        //     return new TaskItem
        //     {

        //     };
        // }
    }
}