using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using go_han.Data;
using go_han.Models;
using Microsoft.EntityFrameworkCore;
using go_han.DTOs.Projects;
using go_han.Repositories.IRepositories;

namespace go_han.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;

        public ProjectRepository(AppDbContext context)
        {
            _context = context;
        }

        // GET ALL PROJECTS
        public async Task<List<ProjectListDto>> GetAllProjectsAsync()
        {
            return await _context.Projects
                .Select(p => new ProjectListDto
                {
                    Id = p.Id,
                    ProjectName = p.ProjectName,
                    Description = p.Description,
                    LeadName = p.Lead.Username,
                    CoLeadName = p.CoLead != null ? p.CoLead.Username : null,
                    Status = p.Status,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate
                })
                .ToListAsync();
        }

        // CREATE PROJECT
        public async Task<int> CreateProjectAsync(CreateProjectsDto dto)
        {
            var project = new Project
            {
                ProjectName = dto.ProjectName,
                Description = dto.Description,
                LeadId = dto.LeadId,
                CoLeadId = dto.CoLeadId,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Status = "Active"
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return project.Id;
        }

        // GET PROJECT DETAIL
        public async Task<ProjectDetailDto?> GetProjectByIdAsync(int projectId)
        {
            return await _context.Projects
                .Where(p => p.Id == projectId)
                .Select(p => new ProjectDetailDto
                {
                    Id = p.Id,
                    ProjectName = p.ProjectName,
                    Description = p.Description,
                    Status = p.Status,
                    Lead = p.Lead.Username,
                    CoLead = p.CoLead != null ? p.CoLead.Username : null,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    Members = p.Members.Select(m => new ProjectMemberDto
                    {
                        UserId = m.User.Id,
                        Username = m.User.Username,
                        Division = m.Division.DivisionName
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<List<ProjectListDto>> GetProjectsByStatusAsync(string status)
        {
            return await _context.Projects
                .Where(p => p.Status.Equals(status, StringComparison.OrdinalIgnoreCase))
                .Select(p => new ProjectListDto
                {
                    Id = p.Id,
                    ProjectName = p.ProjectName,
                    Description = p.Description,
                    LeadName = p.Lead.Username,
                    CoLeadName = p.CoLead != null ? p.CoLead.Username : null,
                    Status = p.Status,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate
                })
                .ToListAsync();
        }

        public async Task<List<ProjectMemberDto>> GetProjectMembersAsync(int projectId)
        {
            return await _context.ProjectMembers
                .Where(pm => pm.ProjectId == projectId)
                .Select(pm => new ProjectMemberDto
                {
                    UserId = pm.User.Id,
                    Username = pm.User.Username,
                    Division = pm.Division.DivisionName
                })
                .ToListAsync();
        }

        public async Task<List<ProjectListDto>> GetProjectsByUserIdAsync(int userId)
        {
            return await _context.Projects
                .Where(p => p.LeadId == userId || p.CoLeadId == userId || p.Members.Any(m => m.UserId == userId))
                .Select(p => new ProjectListDto
                {
                    Id = p.Id,
                    ProjectName = p.ProjectName,
                    Description = p.Description,
                    LeadName = p.Lead.Username,
                    CoLeadName = p.CoLead != null ? p.CoLead.Username : null,
                    Status = p.Status,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate
                })
                .ToListAsync();
        }
        public async Task<bool> AddProjectMembersAsync(int projectId, List<AddProjectsMember> members)
        {
            foreach (var m in members)
            {
                _context.ProjectMembers.Add(new ProjectMember
                {
                    ProjectId = projectId,
                    UserId = m.UserId,
                    DivisionId = m.DivisionId
                });
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveProjectMemberAsync(int projectId, int userId)
        {
            var member = await _context.ProjectMembers
                .FirstOrDefaultAsync(pm => pm.ProjectId == projectId && pm.UserId == userId);
            if (member == null)
            {
                return false;
            }

            _context.ProjectMembers.Remove(member);
            await _context.SaveChangesAsync();
            return true;
        }
        
    }
}
