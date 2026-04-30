using Microsoft.AspNetCore.Mvc;
using TaskApp.Service;

namespace TaskApplicationWebAPI.Controllers
{
    [ApiController]
    [Route("tasks/[controller]")]
    public class TaskController : ControllerBase
    {
        public readonly TaskService taskService;
        public TaskController(TaskService _context) {
            taskService = _context;
        }
        [HttpGet("GetTasks")]
        public IActionResult GetTasks()
        {
            return Ok(taskService.GetTasksInPage(0,10));
        }
    }
}
