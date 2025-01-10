using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Repositories.Interfaces
{
    public interface ITaskRepository:IGenericRepository<TaskItem>
    {
        Task<IEnumerable<TaskItem>> GetCompletedTasks();
    }
}