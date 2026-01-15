using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using go_han.Data;
using go_han.Models;
using Microsoft.EntityFrameworkCore;
using go_han.DTOs.Projects;
using go_han.Repsitories.IRepositories;

namespace go_han.Repsitories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;

        public ProjectRepository(AppDbContext context)
        {
            _context = context;
        }

        // GET ALL PROJECTS
        public async Task<List<Project>> GetAllProjectsAsync()
        {
            return await _context.Projects
                .Include(p => p.Lead)
                .Include(p => p.CoLead)
                .ToListAsync();
        }

        // CREATE PROJECT
        public async Task<Project?> CreateProjectAsync(CreateProjectsDto dto)
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
            return project;
        }

        // GET PROJECT DETAIL
        public async Task<Project?> GetProjectByIdAsync(int projectId)
        {
            return await _context.Projects
                .Include(p => p.Lead)
                .Include(p => p.CoLead)
                .Include(p => p.Members)
                    .ThenInclude(m => m.User)
                .Include(p => p.Members)
                    .ThenInclude(m => m.Division)
                .FirstOrDefaultAsync(p => p.Id == projectId);
        }

        public async Task<List<Project>> GetProjectsByStatusAsync(string status)
        {
            return await _context.Projects
                .Where(p => p.Status.Equals(status, StringComparison.OrdinalIgnoreCase))
                .Include(p => p.Lead)
                .Include(p => p.CoLead)
                .ToListAsync();
        }

        public async Task<List<ProjectMember>> GetProjectMembersAsync(int projectId)
        {
            return await _context.ProjectMembers
                .Where(pm => pm.ProjectId == projectId)
                .Include(pm => pm.User)
                .Include(pm => pm.Division)
                .ToListAsync();
        }

        public async Task<List<Project>> GetProjectsByUserIdAsync(int userId)
        {
            return await _context.Projects
                .Where(p => p.LeadId == userId || p.CoLeadId == userId || p.Members.Any(m => m.UserId == userId))
                .Include(p => p.Lead)
                .Include(p => p.CoLead)
                .ToListAsync();
        }

        public async Task<bool> AddProjectMembersAsync(int projectId, List<AddProjectsMember> members)
        {
            // Verify project exists
            var project = await _context.Projects.FindAsync(projectId);
            if (project == null)
                return false;

            foreach (var m in members)
            {
                // Check if member already exists
                var existingMember = await _context.ProjectMembers
                    .FirstOrDefaultAsync(pm => pm.ProjectId == projectId && pm.UserId == m.UserId);

                if (existingMember == null)
                {
                    _context.ProjectMembers.Add(new ProjectMember
                    {
                        ProjectId = projectId,
                        UserId = m.UserId,
                        DivisionId = m.DivisionId
                    });
                }
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
