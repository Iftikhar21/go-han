using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using go_han.Repsitories.IRepositories;
using go_han.Models;
using go_han.DTOs.Projects;
using go_han.DTOs;
using Microsoft.AspNetCore.Authorization;


namespace go_han.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var projects = await _projectRepository.GetAllProjectsAsync();
            var response = new ApiResponse<List<ProjectListDto>>
            {
                Success = true,
                Message = "Projects retrieved successfully",
                Data = projects
            };
            return Ok(response);
        }

        [HttpGet("{projectId}")]
        public async Task<IActionResult> GetProjectById(int projectId)
        {
            var project = await _projectRepository.GetProjectByIdAsync(projectId);
            var response = new ApiResponse<ProjectDetailDto?>
            {
                Success = project != null,
                Message = project != null ? "Project retrieved successfully" : "Project not found",
                Data = project
            };
            return Ok(response);
        }

        [HttpGet("{projectId}/members")]
        public async Task<IActionResult> GetProjectMembers(int projectId)
        {
            var members = await _projectRepository.GetProjectMembersAsync(projectId);
            var response = new ApiResponse<List<ProjectMemberDto>>
            {
                Success = true,
                Message = "Project members retrieved successfully",
                Data = members
            };
            return Ok(response);
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetProjectsByStatus(string status)
        {
            var projects = await _projectRepository.GetProjectsByStatusAsync(status);
            var response = new ApiResponse<List<ProjectListDto>>
            {
                Success = true,
                Message = "Projects retrieved successfully",
                Data = projects
            };
            return Ok(response);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetProjectsByUserId(int userId)
        {
            var projects = await _projectRepository.GetProjectsByUserIdAsync(userId);
            var response = new ApiResponse<List<ProjectListDto>>
            {
                Success = true,
                Message = "Projects retrieved successfully",
                Data = projects
            };
            return Ok(response);
        }

        [HttpPost("{projectId}/members")]
        public async Task<IActionResult> AddProjectMembers(int projectId, [FromBody] List<AddProjectsMember> members)
        {
            var result = await _projectRepository.AddProjectMembersAsync(projectId, members);
            var response = new ApiResponse<bool>
            {
                Success = true,
                Message = "Project members added successfully",
                Data = result
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectsDto dto)
        {
            var result = await _projectRepository.CreateProjectAsync(dto);
            var response = new ApiResponse<int>
            {
                Success = true,
                Message = "Project created successfully",
                Data = result
            };
            return Ok(response);
        }

        [HttpDelete("{projectId}/members/{userId}")]
        public async Task<IActionResult> RemoveProjectMember(int projectId, int userId)
        {
            var result = await _projectRepository.RemoveProjectMemberAsync(projectId, userId);
            var response = new ApiResponse<bool>
            {
                Success = true,
                Message = "Project member removed successfully",
                Data = result
            };
            return Ok(response);
        }
    }
}