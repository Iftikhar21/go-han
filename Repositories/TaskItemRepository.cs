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

        public async Task<List<TaskItem>> GetAll()
        {
            return await _context.TaskItems.Include(p => p.Project).Include(u => u.Assignee).Include(u => u.ApprovedBy).ToListAsync();
        }

        public async Task<TaskItem?> GetById(int id)
        {
            return await _context.TaskItems.Include(p => p.Project).Include(u => u.Assignee).Include(u => u.ApprovedBy).FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<List<TaskItem>> GetByStatus(int status)
        {
            return await _context.TaskItems.Include(p => p.Project).Include(u => u.Assignee).Include(u => u.ApprovedBy).Where(p => p.Status == status).ToListAsync();
        }

        public async Task<List<TaskItem>> GetByProjectId(int id)
        {
            return await _context.TaskItems.Include(p => p.Project).Include(u => u.Assignee).Include(u => u.ApprovedBy).Where(p => p.ProjectId == id).ToListAsync();
        }

        public async Task CreateTask(TaskItem item)
        {
            _context.TaskItems.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task Remove(TaskItem item)
        {
            _context.Remove(item);
            await _context.SaveChangesAsync();
        }

    }
}