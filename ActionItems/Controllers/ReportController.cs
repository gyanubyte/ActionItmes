using ActionItems.Intefaces;
using ActionItems.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ActionItems.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IActionTaskService _actionTaskService;
        public ReportController( IActionTaskService actionTaskService) 
        {
            _actionTaskService = actionTaskService;
        }



        [HttpPost("AddTask")]
        public async Task<IActionResult> AddTask([FromBody] Issus issus)
        {
            // var result = _actionTask.AddTaskAsync(actionTask);
            if (issus != null)
            {
                var result = _actionTaskService.AddTask(issus);
                return Ok(result);
            }
            return BadRequest();
           
        }
        [HttpGet("GetAllTask")]
        public async Task<IActionResult> GetAllTask()
        {
            var result = _actionTaskService.GetAllTasksAsync();
            return Ok(result);
        }

        [HttpPut("UpdateTask")]
        public async Task<IActionResult> UpdateTask(Issus issus)
        {
            var result = _actionTaskService.UpdateTaskAsync(issus);
            return Ok(result);
        }

        [HttpGet("{taskId}")]
        public async Task<IActionResult> GetTaskByIdAsync(int taskId)
        {
            var result = _actionTaskService.GetTaskByIdAsync(taskId);
            return Ok(result);
        }

        [HttpGet("searchTask")]
        public async Task<IActionResult> SearchTask(string searchItem)
        {
            var result = _actionTaskService.SearchTask(searchItem);
            return Ok(result);
        }


    }
}
