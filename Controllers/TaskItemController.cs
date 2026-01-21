using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using go_han.DTOs.TaskDTOs;
using go_han.Mappers;
using go_han.Repositories.IRepository;
using go_han.Repsitories.IRepositories;
using go_han.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace go_han.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/tasks")]
    [EnableRateLimiting("fixed")]
    public class TaskItemController : ControllerBase
    {
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;

        public TaskItemController(ITaskItemRepository taskItemRepository, IUserRepository userRepository, IProjectRepository projectRepository)
        {
            _taskItemRepository = taskItemRepository ?? throw new ArgumentNullException(nameof(taskItemRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _taskItemRepository.GetAllTaskAsync();
            var response = tasks.Select(task => TaskItemMapper.TaskResponse(task)).ToList();
            return Ok(ResponseResult.Success(response, "Successfully retrieve data"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _taskItemRepository.GetTaskByIdAsync(id);

            if (task == null)
                return NotFound(ResponseResult.Fail<TaskResponseDTO>("Data not found.."));

            var response = TaskItemMapper.TaskResponse(task);
            return Ok(ResponseResult.Success(response, "Successfully retrieve data"));
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetTaskByStatus(int status)
        {
            var tasks = await _taskItemRepository.GetTaskByStatusAsync(status);

            if (tasks == null)
                return NotFound(ResponseResult.Fail<TaskResponseDTO>("Data not found.."));

            var response = tasks.Select(task => TaskItemMapper.TaskResponse(task)).ToList();
            return Ok(ResponseResult.Success(response, "Succesfully retrieve data"));
        }

        [HttpGet("project/{id}")]
        public async Task<IActionResult> GetTaskByProjectId(int id)
        {
            var tasks = await _taskItemRepository.GetTaskByProjectIdAsync(id);

            if (tasks == null)
                return NotFound(ResponseResult.Fail<TaskResponseDTO>("Data not found.."));

            var response = tasks.Select(task => TaskItemMapper.TaskResponse(task)).ToList();
            return Ok(ResponseResult.Success(response, "Successfully retrieve data"));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(TaskCreateRequestDTO req)
        {
            var newTask = TaskItemMapper.TaskCreate(req);
            if (newTask == null)
                return BadRequest(ResponseResult.Fail<TaskResponseDTO>("Input Invalid!"));

            var isProject = await _projectRepository.GetProjectByIdAsync(req.ProjectId);
            if (isProject == null)
                return BadRequest(ResponseResult.Fail<TaskResponseDTO>("Project Id not found.."));

            var isAssigneeIdValid = await _userRepository.GetUserByIdAsync(req.AssigneeId);
            if (isAssigneeIdValid == null)
                return BadRequest(ResponseResult.Fail<TaskResponseDTO>("Assignee Id not found.."));

            var isAssignerIdValid = await _userRepository.GetUserByIdAsync(req.AssignerId);
            if (isAssignerIdValid == null)
                return BadRequest(ResponseResult.Fail<TaskResponseDTO>("Assigner Id not found.."));

            var createdTask = await _taskItemRepository.CreateTaskAsync(newTask);
            var response = TaskItemMapper.TaskResponse(createdTask);
            return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.Id }, ResponseResult.Success(response, "Successfully create task"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskCreateRequestDTO dto)
        {
            var item = TaskItemMapper.TaskCreate(dto);

            var result = await _taskItemRepository.UpdateTaskAsync(id, item);
            if (result == null)
                return NotFound(ResponseResult.Fail<TaskResponseDTO>("Data not found.."));

            var response = TaskItemMapper.TaskResponse(result);
            return Ok(ResponseResult.Success(response, "Successfully update task"));
        }

        [HttpPatch("{id}/assign")]
        public async Task<IActionResult> UpdateAssignTask(int id, int assigneeId)
        {
            var result = await _taskItemRepository.UpdateAssignTaskAsync(id, assigneeId);
            if (result == null)
                return NotFound(ResponseResult.Fail<TaskResponseDTO>("Data not found.."));

            var response = TaskItemMapper.TaskResponse(result);
            return Ok(ResponseResult.Success(response, "Successfully update assignee"));
        }


        [HttpPatch("{id}/start")]
        public async Task<IActionResult> UpdateStatusTask(int id, int status)
        {
            var result = await _taskItemRepository.UpdateStatusTaskAsync(id, status);

            if (result == null)
                return NotFound(ResponseResult.Fail<TaskResponseDTO>("Data not found.."));

            var response = TaskItemMapper.TaskResponse(result);
            return Ok(ResponseResult.Success(response, "Successfully update status"));
        }

        [HttpPut("{id}/submit")]
        public async Task<IActionResult> SubmitTask(int id, int status, string memberComment)
        {
            var result = await _taskItemRepository.UpdateSubmitTaskAsync(id, status, memberComment);
            if (result == null)
                return NotFound(ResponseResult.Fail<TaskResponseDTO>("Data not found.."));

            var response = TaskItemMapper.TaskResponse(result);
            return Ok(ResponseResult.Success(response, "Successfully update status"));
        }

        // PUT: api/tasks/5/approve?status=3&approvedById=10
        [HttpPut("{id}/approve")]
        public async Task<IActionResult> ApproveTask(int id, int status, int approvedById, DateTime? approvedAt = null)
        {
            var result = await _taskItemRepository.UpdateApprovalTaskAsync(id, status, approvedById, approvedAt);
            if (result == null)
                return NotFound(ResponseResult.Fail<TaskResponseDTO>("Data not found.."));

            var response = TaskItemMapper.TaskResponse(result);
            return Ok(ResponseResult.Success(response, "Successfully update status"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _taskItemRepository.DeleteTaskAsync(id);
            if (task == false)
                return NotFound(ResponseResult.Fail<TaskResponseDTO>("User not found"));

            return Ok(ResponseResult.Success("Task deleted"));
        }
    }
}