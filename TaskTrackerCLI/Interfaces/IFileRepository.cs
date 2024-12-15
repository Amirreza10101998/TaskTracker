using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTrackerCLI.Repositories;

namespace TaskTrackerCLI.Interfaces
{
    public interface IFileRepository
    {
        public List<TaskModel> LoadTasks();
        public void SaveTasks(List<TaskModel> tasks);
    }
}