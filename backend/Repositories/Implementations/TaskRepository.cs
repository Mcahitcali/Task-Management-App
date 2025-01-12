using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Models;
using backend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories.Implementations
{
    public class TaskRepository : GenericRepository<TaskItem>, ITaskRepository
    {
        private readonly TaskDbContext _context;
        public TaskRepository(TaskDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await _context.Tasks
                .Include(t => t.User)
                .ToListAsync();
        }

        public override async Task<TaskItem> GetByIdAsync(int id)
        {
            return await _context.Tasks
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<TaskItem>> GetCompletedTasks()
        {
            return await _context.Tasks
                .Include(t => t.User)
                .Where(t => t.IsCompleted)
                .ToListAsync();
        }
    }
}