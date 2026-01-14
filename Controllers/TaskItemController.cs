using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using go_han.DTOs.TaskDTOs;
using go_han.Mappers;
using go_han.Repositories.IRepository;
using go_han.Utils;
using Microsoft.AspNetCore.Mvc;

namespace go_han.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskItemController : ControllerBase
    {
        private readonly ITaskItemRepository _taskItemRepository;
        private readonly IUserRepository _userRepository;

        public TaskItemController(ITaskItemRepository taskItemRepository, IUserRepository userRepository)
        {
            _taskItemRepository = taskItemRepository ?? throw new ArgumentNullException(nameof(taskItemRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
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

            if(task == null)
            return NotFound(ResponseResult.Fail<TaskResponseDTO>("Data not found.."));

            var response = TaskItemMapper.TaskResponse(task);
            return Ok(ResponseResult.Success(response, "Successfully retrieve data"));
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetTaskByStatus(int status)
        {
            var tasks = await _taskItemRepository.GetTaskByStatusAsync(status);

            if(tasks == null)
            return NotFound(ResponseResult.Fail<TaskResponseDTO>("Data not found.."));

            var response = tasks.Select(task => TaskItemMapper.TaskResponse(task)).ToList();
            return Ok(ResponseResult.Success(response, "Succesfully retrieve data"));
        }

        [HttpGet("project/{id}")]
        public async Task<IActionResult> GetTaskByProjectId(int id)
        {
            var tasks = await _taskItemRepository.GetTaskByProjectIdAsync(id);

            if(tasks == null)
            return NotFound(ResponseResult.Fail<TaskResponseDTO>("Data not found.."));

            var response = tasks.Select(task => TaskItemMapper.TaskResponse(task)).ToList();
            return Ok(ResponseResult.Success(response, "Successfully retrieve data"));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(TaskCreateRequestDTO req)
        {
            var newTask = TaskItemMapper.TaskCreate(req);
            if(newTask == null)
            return BadRequest(ResponseResult.Fail<TaskCreateRequestDTO>("Input Invalid!"));

            // var isProject = 
            // if(isProject == null)
            // return BadRequest(ResponseResult.Fail<TaskCreateRequestDTO>("Project Id not found.."));

            var isAssigneeIdValid = await _userRepository.GetUserByIdAsync(req.AssigneeId);
            if(isAssigneeIdValid == null)
            return BadRequest(ResponseResult.Fail<TaskCreateRequestDTO>("Assignee Id not found.."));

            await _taskItemRepository.CreateTaskAsync(newTask);
            return Ok(ResponseResult.Success(newTask, "Successfully create task"));
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