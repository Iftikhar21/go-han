using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using go_han.DTOs.Projects;

namespace go_han.Repositories.IRepositories
{
    public interface IProjectRepository
    {
        Task<int> CreateProjectAsync(CreateProjectsDto createProjectsDto);
        Task<ProjectDetailDto?> GetProjectByIdAsync(int projectId);
        Task<List<ProjectListDto>> GetAllProjectsAsync();
        Task<List<ProjectMemberDto>> GetProjectMembersAsync(int projectId);
        Task<List<ProjectListDto>> GetProjectsByStatusAsync(string status);
        Task<List<ProjectListDto>> GetProjectsByUserIdAsync(int userId);
        Task<bool> AddProjectMembersAsync(int projectId, List<AddProjectsMember> members);
        Task<bool> RemoveProjectMemberAsync(int projectId, int userId);
    }
}
