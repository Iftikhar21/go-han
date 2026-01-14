using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using go_han.Data;
using go_han.Models;
using go_han.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace go_han.Repositories
{
    public class TaskItemRepository : ITaskItemRepository
    {
        private readonly AppDbContext _context;

        public TaskItemRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<TaskItem>> GetAllTaskAsync()
        {
            return await _context.TaskItems.Include(p => p.Project).Include(u => u.Assignee).Include(u => u.ApprovedBy).ToListAsync();
        }

        public async Task<TaskItem?> GetTaskByIdAsync(int id)
        {
            return await _context.TaskItems.Include(p => p.Project).Include(u => u.Assignee).Include(u => u.ApprovedBy).FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<List<TaskItem>> GetTaskByStatusAsync(int status)
        {
            return await _context.TaskItems.Include(p => p.Project).Include(u => u.Assignee).Include(u => u.ApprovedBy).Where(p => p.Status == status).ToListAsync();
        }

        public async Task<List<TaskItem>> GetTaskByProjectIdAsync(int id)
        {
            return await _context.TaskItems.Include(p => p.Project).Include(u => u.Assignee).Include(u => u.ApprovedBy).Where(p => p.ProjectId == id).ToListAsync();
        }

        public async Task<TaskItem> CreateTaskAsync(TaskItem item)
        {
            _context.TaskItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var existTask = await _context.TaskItems.FindAsync(id);
            if(existTask == null)
            return false;

            _context.Remove(existTask);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}