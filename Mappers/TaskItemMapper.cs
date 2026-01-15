using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using go_han.DTOs.Projects;
using go_han.DTOs.Roles;
using go_han.DTOs.TaskDTOs;
using go_han.DTOs.User;
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
                ProjectData = new ProjectDetailDto
                {
                    ProjectName = task.Project.ProjectName,
                    Description = task.Project.Description,
                    Lead = task.Project.Lead.Username,
                    CoLead = task.Project.CoLead != null ? task.Project.CoLead.Username : null,
                    StartDate = task.Project.StartDate,
                    EndDate = task.Project.EndDate
                },
                AssigneeId = task.AssigneeId,
                Assignee = new UserDto
                {
                    Username = task.Assignee.Username,
                    Email = task.Assignee.Email,
                    Role = new RoleReadDto
                    {
                        Id = task.Assignee.Role.Id,
                        RoleName = task.Assignee.Role.RoleName
                    }
                },
                Title = task.Title,
                Content = task.Content,
                Difficulty = task.Difficulty,
                Deadline = task.Deadline,
                Status = task.Status,

                // Approval
                MemberComment = task.MemberComment,
                ApprovedById = task.ApprovedById,
                ApprovedBy = new UserDto
                {
                    Username = task.ApprovedBy.Username,
                    Email = task.ApprovedBy.Email,
                    Role = new RoleReadDto
                    {
                        Id = task.ApprovedBy.Role.Id,
                        RoleName = task.ApprovedBy.Role.RoleName
                    }
                },
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
    }
}