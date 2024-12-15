using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TaskTrackerCLI.Interfaces;

namespace TaskTrackerCLI.Services
{
    public class TaskService : ITaskServiceRepository
    {
        private readonly IFileRepository _fileRepository;

        public TaskService(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }
        public void AddTask(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("Task description cannot be empty.");
            }

            var tasks = _fileRepository.LoadTasks();

            int newId = tasks.Any() ? tasks.Max(t => t.id) + 1 : 1;

            var task = new TaskModel {
                id = newId,
                description = description,
                status = "todo",
                createdAt = DateTime.Now,
                updatedAt = DateTime.Now
            };

            tasks.Add(task);

            _fileRepository.SaveTasks(tasks);

            Console.WriteLine($"Task added successfully (ID: {task.id})");
        }

        public void UpdateTask(int id, string newDescription)
        {
            newDescription = "";

            var tasks = _fileRepository.LoadTasks();

            var task = tasks.FirstOrDefault(t => t.id == id);

            if (task == null)
            {
                throw new KeyNotFoundException($"Task with ID {id} not found.");
            }

            task.description = newDescription;
            task.updatedAt = DateTime.Now;

            _fileRepository.SaveTasks(tasks);

            Console.WriteLine($"Task with ID {id} updated successfully.");
        }

        public void DeleteTask(int id)
        {
            var tasks = _fileRepository.LoadTasks();
            var task = tasks.FirstOrDefault(t => t.id == id);
            
            if (task == null)
            {
                Console.WriteLine($"Task with ID {id} not found.");
                return;
            } 
        
            tasks.Remove(task);
        
            _fileRepository.SaveTasks(tasks);
        
            Console.WriteLine($"Task with ID {id} removed successfully.");
        }

        public void MarkInProgress(int id)
        {
            var tasks = _fileRepository.LoadTasks();
            var task = tasks.FirstOrDefault(t => t.id == id);
            
            if (task == null)
            {
                Console.WriteLine($"Task with ID {id} not found.");
                return;
            }

            task.status = "in-progress";
            task.updatedAt = DateTime.Now;

            _fileRepository.SaveTasks(tasks);
        
            Console.WriteLine($"Task with ID {id} marked as 'in-progress' successfully.");
        }

        public void MarkDone(int id)
        {
            var tasks = _fileRepository.LoadTasks();
            var task = tasks.FirstOrDefault(t => t.id == id);
            
            if (task == null)
            {
                Console.WriteLine($"Task with ID {id} not found.");
                return;
            }

            task.status = "done";
            task.updatedAt = DateTime.Now;

            _fileRepository.SaveTasks(tasks);
        
            Console.WriteLine($"Task with ID {id} marked as 'done' successfully.");
        }

        public void ListTasks(string? status)
        {
            var tasks = _fileRepository.LoadTasks();

            if (tasks == null || !tasks.Any())
            {
                Console.WriteLine("No tasks found.");
                return;
            }

            if(!string.IsNullOrEmpty(status))
            {
                status = status.ToLower();
                tasks = tasks.Where(task => task.status.ToLower() == status).ToList();
                
                if (!tasks.Any())
                {
                    Console.WriteLine($"No tasks with status '{status}' found.");
                    return;
                }
            }

            foreach(var task in tasks)
            {
                Console.WriteLine($"ID: {task.id} | Description: {task.description} | Status: {task.status} | Created: {task.createdAt} | Updated: {task.updatedAt}");
            }
        }

        public TaskModel ValidateTaskExists(int id)
        {
            var tasks = _fileRepository.LoadTasks();
            var task = tasks.FirstOrDefault(t => t.id == id);

            if(task == null)
            {
                throw new ArgumentException($"Task with ID {id} does not exist. Please check the ID and try again.");
            }
            
            return task;
        }
    }
}