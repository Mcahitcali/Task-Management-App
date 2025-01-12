using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using backend.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TasksController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetAllTasks()
        {
            var tasks = await _taskRepository.GetAllAsync();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetTaskById(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
                return NotFound("Task not found");
            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<TaskItem>> CreateTask(CreateTaskRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var task = new TaskItem
            {
                Title = request.Title,
                Description = request.Description,
                CreatedDate = DateTime.UtcNow,
                IsCompleted = false
            };

            await _taskRepository.AddAsync(task);
            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TaskItem>> UpdateTask(int id, UpdateTaskRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingTask = await _taskRepository.GetByIdAsync(id);
            if (existingTask == null)
                return NotFound("Task not found");

            if (request.Title != null)
                existingTask.Title = request.Title;
                
            if (request.Description != null)
                existingTask.Description = request.Description;
            
            if (request.IsCompleted != existingTask.IsCompleted)
            {
                existingTask.IsCompleted = request.IsCompleted;
                existingTask.CompletedDate = request.IsCompleted ? DateTime.UtcNow : null;
            }

            await _taskRepository.UpdateAsync(existingTask);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TaskItem>> DeleteTask(int id)
        {
            await _taskRepository.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("completed")]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetCompletedTasks()
        {
            var tasks = await _taskRepository.GetCompletedTasks();
            return Ok(tasks);
        }
    }
}
