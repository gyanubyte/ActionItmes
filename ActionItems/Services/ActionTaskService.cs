using ActionItems.Context;
using ActionItems.Intefaces;
using ActionItems.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ActionItems.Services
{
    public class ActionTaskService : IActionTaskService
    {
        private readonly ActionContext _actionContext;

        public ActionTaskService(ActionContext actionContext)
        {
            _actionContext = actionContext;
        }
        public Issus AddTask(Issus actionTask)
        {
            var result =   _actionContext.Tasks.Add(actionTask);
             _actionContext.SaveChanges();
            return result.Entity;
        }

        public Task<bool> DeleteTaskAsync(int id)
        {
            throw new NotImplementedException();
        }

        public   List<Issus> GetAllTasksAsync()
        {
            return  _actionContext.Tasks.ToList();
        }

        public  Issus GetTaskByIdAsync(int id)
        {
            return  _actionContext.Tasks.Where(task => task.TaskId == id).FirstOrDefault();
        }

        public List<Issus> SearchTask(string searchItem)
        {
            var result = _actionContext.Tasks.Where(task=>task.Subject.Contains(searchItem) || task.requester.Contains(searchItem)
            || task.Status.Equals(searchItem) || task.Description.Contains(searchItem)).ToList();
            return result;
        }

        public  bool UpdateTaskAsync(Issus actionTask)
        {
            Issus issus= new Issus();
            var exitingTask =  _actionContext.Tasks.Find(actionTask.TaskId);
            if(exitingTask != null)
            {
                exitingTask.requester = actionTask.requester;
                exitingTask.Subject = actionTask.Subject;
                exitingTask.CreatedBy = actionTask.CreatedBy;
                exitingTask.AssignedTo = actionTask.AssignedTo;
                exitingTask.Priority = actionTask.Priority;
                exitingTask.Description = actionTask.Description;
                exitingTask.DueDate = actionTask.DueDate;
                 _actionContext.SaveChanges();

                return true;
            }
            return false;
        }
    }
}
