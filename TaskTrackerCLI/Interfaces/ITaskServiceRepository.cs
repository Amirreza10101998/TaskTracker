using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTrackerCLI.Services;

namespace TaskTrackerCLI.Interfaces
{
    public interface ITaskServiceRepository
    {
        public void AddTask(string description);
        public void UpdateTask(int id, string newDescription);
        public void DeleteTask(int id);
        public void MarkInProgress(int id);
        public void MarkDone(int id);
        public void ListTasks(string? status);
        public TaskModel ValidateTaskExists(int id);
    }
}