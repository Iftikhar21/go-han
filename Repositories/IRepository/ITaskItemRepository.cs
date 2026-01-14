using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using go_han.Models;

namespace go_han.Repositories.IRepository
{
    public interface ITaskItemRepository
    {
        Task<List<TaskItem>> GetAllTaskAsync();
        Task<TaskItem?> GetTaskByIdAsync(int id);
        Task<List<TaskItem>> GetTaskByStatusAsync(int status);
        Task<List<TaskItem>> GetTaskByProjectIdAsync(int id);
        Task<TaskItem> CreateTaskAsync(TaskItem item);
        Task<bool> DeleteTaskAsync(int id);
        // SISA EDIT
    }
}