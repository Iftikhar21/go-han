using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using go_han.DTOs.Projects;
using go_han.Models;

namespace go_han.Repsitories.IRepositories
{
    public interface IProjectRepository
    {
        public Task<Project?> CreateProjectAsync(CreateProjectsDto createProjectsDto);
        public Task<Project?> GetProjectByIdAsync(int projectId);
        public Task<List<Project>> GetAllProjectsAsync();
        public Task<List<ProjectMember>> GetProjectMembersAsync(int projectId);
        public Task<List<Project>> GetProjectsByStatusAsync(string status);
        public Task<List<Project>> GetProjectsByUserIdAsync(int userId);
        public Task<bool> AddProjectMembersAsync(int projectId, List<AddProjectsMember> members);
        public Task<bool> RemoveProjectMemberAsync(int projectId, int userId);
        public Task<bool> DeleteProjectAsync(int projectId);
    }
}

