using System;
using System.Reflection;
using TaskTrackerCLI.Repositories;
using TaskTrackerCLI.Services;

class Program
{
    static void Main(string [] args)
    {
        var fileRepository = new FileRepository();
        var taskService = new TaskService(fileRepository);

                if (args.Length == 0)
        {
            Console.WriteLine("No command provided. Use 'help' to see available commands.");
            return;
        }

        string command = args[0].ToLower();

        try
        {
            switch (command)
            {
                case "add":
                    if (args.Length < 2)
                        throw new ArgumentException("Description is required for adding a task.");
                    string description = args[1];
                    taskService.AddTask(description);
                    break;

                case "update":
                    if (args.Length < 3)
                        throw new ArgumentException("ID and new description are required for updating a task.");
                    int updateId = int.Parse(args[1]);
                    string newDescription = args[2];
                    taskService.UpdateTask(updateId, newDescription);
                    break;

                case "delete":
                    if (args.Length < 2)
                        throw new ArgumentException("ID is required to delete a task.");
                    int deleteId = int.Parse(args[1]);
                    taskService.DeleteTask(deleteId);
                    break;

                case "mark-in-progress":
                    if (args.Length < 2)
                        throw new ArgumentException("ID is required to mark a task as in progress.");
                    int progressId = int.Parse(args[1]);
                    taskService.MarkInProgress(progressId);
                    break;

                case "mark-done":
                    if (args.Length < 2)
                        throw new ArgumentException("ID is required to mark a task as done.");
                    int doneId = int.Parse(args[1]);
                    taskService.MarkDone(doneId);
                    break;

                case "list":
                    string? status = args.Length > 1 ? args[1].ToLower() : null;
                    taskService.ListTasks(status);
                    break;

                case "help":
                    DisplayHelp();
                    break;

                default:
                    Console.WriteLine($"Unknown command: {command}. Use 'help' to see available commands.");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void DisplayHelp()
    {
        Console.WriteLine("Available commands:");
        Console.WriteLine("  add <description>         - Add a new task");
        Console.WriteLine("  update <id> <description> - Update an existing task");
        Console.WriteLine("  delete <id>               - Delete a task");
        Console.WriteLine("  mark-in-progress <id>     - Mark a task as in progress");
        Console.WriteLine("  mark-done <id>            - Mark a task as done");
        Console.WriteLine("  list [status]             - List tasks (optional: by status)");
        Console.WriteLine("  help                      - Display this help menu");
    }
}