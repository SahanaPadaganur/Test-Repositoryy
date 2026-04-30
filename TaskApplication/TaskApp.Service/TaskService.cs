using System;
using System.Collections.Generic;
using System.Text;
using TaskApp.DAL;
using TaskApp.Model;

namespace TaskApp.Service
{
    public class TaskService
    {
        public readonly TaskApplicationContext taskApplicationContext;
        public TaskService(TaskApplicationContext _context) { 
            taskApplicationContext = _context;
        }

        public List<TaskModel> GetTasksInPage(int pageIndex, int pageSize)
        {
            List<TaskModel> tasks = new List<TaskModel>();
            tasks= taskApplicationContext.Tasks.Skip(pageIndex).Take(pageSize).ToList();
            return tasks;
        }
    }
}
