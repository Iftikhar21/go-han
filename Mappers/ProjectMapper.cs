using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using go_han.DTOs.Projects;
using go_han.Models;

namespace go_han.Mappers
{
    public static class ProjectMapper
    {
        public static ProjectListDto ToProjectListDto(this Project project)
        {
            return new ProjectListDto
            {
                Id = project.Id,
                ProjectName = project.ProjectName,
                Description = project.Description,
                LeadName = project.Lead.Username,
                CoLeadName = project.CoLead != null ? project.CoLead.Username : null,
                Status = project.Status,
                StartDate = project.StartDate,
                EndDate = project.EndDate
            };
        }

        public static ProjectDetailDto ToProjectDetailDto(this Project project)
        {
            return new ProjectDetailDto
            {
                Id = project.Id,
                ProjectName = project.ProjectName,
                Description = project.Description,
                Status = project.Status,
                Lead = project.Lead.Username,
                CoLead = project.CoLead != null ? project.CoLead.Username : null,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Members = project.Members.Select(m => new ProjectMemberDto
                {
                    UserId = m.User.Id,
                    Username = m.User.Username,
                    Division = m.Division.DivisionName
                }).ToList()
            };
        }

        public static ProjectMemberDto ToProjectMemberDto(this ProjectMember member)
        {
            return new ProjectMemberDto
            {
                UserId = member.User.Id,
                Username = member.User.Username,
                Division = member.Division.DivisionName
            };
        }

        public static Project ToProject(this CreateProjectsDto dto)
        {
            return new Project
            {
                ProjectName = dto.ProjectName,
                Description = dto.Description,
                LeadId = dto.LeadId,
                CoLeadId = dto.CoLeadId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Status = "Active"
            };
        }
    }
}
