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
        Task<Project?> CreateProjectAsync(CreateProjectsDto createProjectsDto);
        Task<Project?> GetProjectByIdAsync(int projectId);
        Task<List<Project>> GetAllProjectsAsync();
        Task<List<ProjectMember>> GetProjectMembersAsync(int projectId);
        Task<List<Project>> GetProjectsByStatusAsync(string status);
        Task<List<Project>> GetProjectsByUserIdAsync(int userId);
        Task<bool> AddProjectMembersAsync(int projectId, List<AddProjectsMember> members);
        Task<bool> RemoveProjectMemberAsync(int projectId, int userId);
    }
}

