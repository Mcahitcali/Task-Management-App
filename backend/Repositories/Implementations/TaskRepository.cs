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
    public class TaskRepository:GenericRepository<TaskItem>,ITaskRepository
    {
        private readonly TaskDbContext _context;
        public TaskRepository(TaskDbContext context):base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskItem>> GetCompletedTasks()
        {
            return await _context.Tasks.Where(t => t.IsCompleted).ToListAsync();
        }
        
    }
}