using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using go_han.DTOs.TaskDTOs;
using go_han.Models;

namespace go_han.Repositories.IRepository
{
    public interface ITaskItemRepository
    {
        public Task<List<TaskItem>> GetAllTaskAsync();
        public Task<TaskItem?> GetTaskByIdAsync(int id);
        public Task<List<TaskItem>> GetTaskByStatusAsync(int status);
        public Task<List<TaskItem>> GetTaskByProjectIdAsync(int id);
        public Task<TaskItem> CreateTaskAsync(TaskItem item);
        public Task<TaskItem?> UpdateStatusTaskAsync(int id, int status);
        public Task<TaskItem?> UpdateApprovalTaskAsync(int id, int status, int approvedById, DateTime? approvedAt);
        public Task<TaskItem?> UpdateSubmitTaskAsync(int id, int status, string memberComment);
        public Task<bool> DeleteTaskAsync(int id);
    }
}