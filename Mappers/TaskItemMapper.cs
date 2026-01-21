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
            RoleReadDto? assigneeRole = null;
            if (task.Assignee?.Role != null)
            {
                assigneeRole = new RoleReadDto
                {
                    Id = task.Assignee.Role.Id,
                    RoleName = task.Assignee.Role.RoleName
                };
            }

            UserDto? assigneeDto = null;
            if (task.Assignee != null)
            {
                assigneeDto = new UserDto
                {
                    Id = task.Assignee.Id,
                    Username = task.Assignee.Username,
                    Email = task.Assignee.Email,
                    Role = assigneeRole
                };
            }

            UserDto? assigner = null;
            if (task.Assigner != null)
            {
                assigner = new UserDto
                {
                    Id = task.Assigner.Id,
                    Username = task.Assigner.Username,
                    Email = task.Assigner.Email,
                    Role = assigneeRole
                };
            }

            RoleReadDto? approvedByRole = null;
            if (task.ApprovedBy?.Role != null)
            {
                approvedByRole = new RoleReadDto
                {
                    Id = task.ApprovedBy.Role.Id,
                    RoleName = task.ApprovedBy.Role.RoleName
                };
            }

            UserDto? approvedByDto = null;
            if (task.ApprovedBy != null)
            {
                approvedByDto = new UserDto
                {
                    Id = task.ApprovedBy.Id,
                    Username = task.ApprovedBy.Username,
                    Email = task.ApprovedBy.Email,
                    Role = approvedByRole
                };
            }

            ProjectDetailDto? projectDto = null;
            if (task.Project != null)
            {
                var membersList = new List<ProjectMemberDto>();
                if (task.Project.Members != null)
                {
                    foreach (var member in task.Project.Members)
                    {
                        membersList.Add(new ProjectMemberDto
                        {
                            UserId = member.User.Id,
                            Username = member.User.Username,
                            Division = member.Division.DivisionName
                        });
                    }
                }

                projectDto = new ProjectDetailDto
                {
                    Id = task.Project.Id,
                    ProjectName = task.Project.ProjectName,
                    Description = task.Project.Description,
                    Status = task.Project.Status,
                    Lead = task.Project.Lead?.Username ?? "",
                    CoLead = task.Project.CoLead?.Username,
                    StartDate = task.Project.StartDate,
                    EndDate = task.Project.EndDate,
                    Members = membersList
                };
            }

            
            string statusInString = "";
            if(task.Status == 0 ) statusInString = "Todo";
            else if (task.Status == 1) statusInString = "In Progress";
            else if (task.Status == 2) statusInString = "Member Approved";
            else if (task.Status == 3) statusInString = "Fully Completed";

            return new TaskResponseDTO
            {
                Id = task.Id,
                ProjectId = task.ProjectId,
                ProjectData = projectDto,
                AssigneeId = task.AssigneeId,
                Assignee = assigneeDto,
                AssignerId = task.AssignerId,
                Assigner = assigner,
                Title = task.Title,
                Content = task.Content,
                Difficulty = task.Difficulty,
                Deadline = task.Deadline,
                Status = statusInString,
                MemberComment = task.MemberComment,
                ApprovedById = task.ApprovedById,
                ApprovedBy = approvedByDto,
                ApprovedAt = task.ApprovedAt,
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