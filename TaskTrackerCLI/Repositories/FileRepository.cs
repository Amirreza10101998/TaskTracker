using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TaskTrackerCLI.Interfaces;

namespace TaskTrackerCLI.Repositories
{
    public class FileRepository : IFileRepository
    {
        private const string filePath = "./TaskData.json";

        public List<TaskModel> LoadTasks()
        {
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "[]");
                Console.WriteLine("Task data file not found. A new file has been created.");
                return new List<TaskModel>(); 
            }
            try
            {
                string json = File.ReadAllText(filePath);
                var tasks = JsonSerializer.Deserialize<List<TaskModel>>(json) ?? new List<TaskModel>();
                return tasks;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading tasks: {ex.Message}");
                return new List<TaskModel>();
            }
        }

        public void SaveTasks(List<TaskModel> tasks)
        {
            try
            {
                string json = JsonSerializer.Serialize(tasks);
                File.WriteAllText(filePath, json);
                
                Console.WriteLine("Tasks saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving tasks: {ex.Message}");
            }
        }
    }
}