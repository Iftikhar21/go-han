using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using go_han.Repsitories.IRepositories;
using go_han.Models;
using go_han.DTOs.Projects;


namespace go_han.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        [HttpGet("{projectId}/members")]
        public async Task<IActionResult> GetProjectMembers(int projectId)
        {
            var members = await _projectRepository.GetProjectMembersAsync(projectId);
            return Ok(members);
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetProjectsByStatus(string status)
        {
            var projects = await _projectRepository.GetProjectsByStatusAsync(status);
            return Ok(projects);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetProjectsByUserId(int userId)
        {
            var projects = await _projectRepository.GetProjectsByUserIdAsync(userId);
            return Ok(projects);
        }

        [HttpPost("{projectId}/members")]
        public async Task<IActionResult> AddProjectMembers(int projectId, [FromBody] List<AddProjectsMember> members)
        {
            var result = await _projectRepository.AddProjectMembersAsync(projectId, members);
            return Ok(result);
        }

        [HttpDelete("{projectId}/members/{userId}")]
        public async Task<IActionResult> RemoveProjectMember(int projectId, int userId)
        {
            var result = await _projectRepository.RemoveProjectMemberAsync(projectId, userId);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var projects = await _projectRepository.GetAllProjectsAsync();
            return Ok(projects);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectsDto dto)
        {
            var result = await _projectRepository.CreateProjectAsync(dto);
            return Ok(result);
        }


        [HttpGet("{projectId}")]
        public async Task<IActionResult> GetProjectById(int projectId)
        {
            var project = await _projectRepository.GetProjectByIdAsync(projectId);
            return Ok(project);
        }
    }
}