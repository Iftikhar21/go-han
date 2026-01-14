using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using go_han.Models;

namespace go_han.Repositories.IRepository
{
    public interface ITaskItemRepository
    {
        Task<List<TaskItem>> GetAll();
        Task<TaskItem?> GetById(int id);
        Task<List<TaskItem>> GetByStatus(int status);
        Task<List<TaskItem>> GetByProjectId(int id);
        Task CreateTask(TaskItem item);
        Task Remove(TaskItem item);
        // SISA EDIT
    }
}