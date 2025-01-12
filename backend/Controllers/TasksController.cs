using System;
using System.Collections.Generic;
using System.Security.Claims;
using backend.Models;
using backend.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TasksController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        private int GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            return int.Parse(userIdClaim?.Value ?? throw new UnauthorizedAccessException("User not authenticated"));
        }

        private bool IsAdmin()
        {
            return User.IsInRole("Admin");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetAllTasks()
        {
            var tasks = await _taskRepository.GetAllAsync();
            if (tasks == null)
                return StatusCode(500, "Error retrieving tasks");

            // Filter tasks based on user role
            if (!IsAdmin())
            {
                var userId = GetUserId();
                tasks = tasks.Where(t => t.UserId == userId);
            }

            if (!tasks.Any())
                return NotFound("No tasks found");
                
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetTaskById(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
                return NotFound("Task not found");

            // Check if user has permission to view this task
            if (!IsAdmin() && task.UserId != GetUserId())
                return Forbid();

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
                IsCompleted = false,
                UserId = GetUserId()
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

            // Check if user has permission to update this task
            if (!IsAdmin() && existingTask.UserId != GetUserId())
                return Forbid();

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
            return Ok(existingTask);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TaskItem>> DeleteTask(int id)
        {
            var existingTask = await _taskRepository.GetByIdAsync(id);
            if (existingTask == null)
                return NotFound("Task not found");

            // Check if user has permission to delete this task
            if (!IsAdmin() && existingTask.UserId != GetUserId())
                return Forbid();
                
            await _taskRepository.DeleteAsync(id);
            return Ok();
        }

        [HttpGet("completed")]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetCompletedTasks()
        {
            var tasks = await _taskRepository.GetCompletedTasks();
            
            if (tasks == null)
                return StatusCode(500, "Error retrieving completed tasks");

            // Filter tasks based on user role
            if (!IsAdmin())
            {
                var userId = GetUserId();
                tasks = tasks.Where(t => t.UserId == userId);
            }
                
            if (!tasks.Any())
                return NotFound("No completed tasks found");
                
            return Ok(tasks);
        }
    }
}
