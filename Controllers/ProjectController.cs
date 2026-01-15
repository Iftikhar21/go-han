using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using go_han.Repsitories.IRepositories;
using go_han.Models;
using go_han.DTOs.Projects;
using go_han.DTOs;
using go_han.Mappers;
using go_han.Utils;
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
            if (!projects.Any())
                return Ok(ResponseResult.Success(new List<ProjectListDto>(), "No projects found"));

            var projectsDto = projects.Select(x => x.ToProjectListDto()).ToList();
            return Ok(ResponseResult.Success(projectsDto, "Projects retrieved successfully"));
        }

        [HttpGet("{projectId}")]
        public async Task<IActionResult> GetProjectById(int projectId)
        {
            var project = await _projectRepository.GetProjectByIdAsync(projectId);
            if (project == null)
                return NotFound(ResponseResult.Fail<ProjectDetailDto>("Project not found"));

            var projectDto = project.ToProjectDetailDto();
            return Ok(ResponseResult.Success(projectDto, "Project retrieved successfully"));
        }

        [HttpGet("{projectId}/members")]
        public async Task<IActionResult> GetProjectMembers(int projectId)
        {
            var members = await _projectRepository.GetProjectMembersAsync(projectId);
            if (!members.Any())
                return Ok(ResponseResult.Success(new List<ProjectMemberDto>(), "No members found"));

            var membersDto = members.Select(x => x.ToProjectMemberDto()).ToList();
            return Ok(ResponseResult.Success(membersDto, "Project members retrieved successfully"));
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetProjectsByStatus(string status)
        {
            var projects = await _projectRepository.GetProjectsByStatusAsync(status);
            if (!projects.Any())
                return Ok(ResponseResult.Success(new List<ProjectListDto>(), "No projects found with this status"));

            var projectsDto = projects.Select(x => x.ToProjectListDto()).ToList();
            return Ok(ResponseResult.Success(projectsDto, "Projects retrieved successfully"));
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetProjectsByUserId(int userId)
        {
            var projects = await _projectRepository.GetProjectsByUserIdAsync(userId);
            if (!projects.Any())
                return Ok(ResponseResult.Success(new List<ProjectListDto>(), "No projects found for this user"));

            var projectsDto = projects.Select(x => x.ToProjectListDto()).ToList();
            return Ok(ResponseResult.Success(projectsDto, "Projects retrieved successfully"));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectsDto dto)
        {
            if (dto == null)
                return BadRequest(ResponseResult.Fail<ProjectDetailDto>("Invalid project data"));

            var project = await _projectRepository.CreateProjectAsync(dto);
            if (project == null)
                return BadRequest(ResponseResult.Fail<ProjectDetailDto>("Failed to create project"));
                
            var getProject = await _projectRepository.GetProjectByIdAsync(project.Id);
            if (getProject == null)
                return NotFound(ResponseResult.Fail<ProjectDetailDto>("Project not found"));

            var projectDto = getProject.ToProjectDetailDto();
            return CreatedAtAction(
                nameof(GetProjectById),
                new { projectId = project.Id },
                ResponseResult.Success(projectDto, "Project created successfully")
            );
        }

        [HttpPost("{projectId}/members")]
        public async Task<IActionResult> AddProjectMembers(int projectId, [FromBody] List<AddProjectsMember> members)
        {
            if (members == null || !members.Any())
                return BadRequest(ResponseResult.Fail<bool>("No members provided"));

            var result = await _projectRepository.AddProjectMembersAsync(projectId, members);
            if (!result)
                return BadRequest(ResponseResult.Fail<bool>("Failed to add project members"));

            return Ok(ResponseResult.Success(true, "Project members added successfully"));
        }

        [HttpDelete("{projectId}/members/{userId}")]
        public async Task<IActionResult> RemoveProjectMember(int projectId, int userId)
        {
            var result = await _projectRepository.RemoveProjectMemberAsync(projectId, userId);
            if (!result)
                return NotFound(ResponseResult.Fail<bool>("Project member not found"));

            return Ok(ResponseResult.Success(true, "Project member removed successfully"));
        }
    }
}