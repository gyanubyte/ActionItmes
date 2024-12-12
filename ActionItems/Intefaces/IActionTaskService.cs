using ActionItems.Models;

namespace ActionItems.Intefaces
{
    public interface IActionTaskService
    {
        Issus AddTask(Issus actionTask);

        Issus GetTaskByIdAsync(int id);

        List<Issus> GetAllTasksAsync();

        bool UpdateTaskAsync(Issus actionTask);

        Task<bool> DeleteTaskAsync(int id);

        List<Issus> SearchTask(string searchItem);
    }
}
