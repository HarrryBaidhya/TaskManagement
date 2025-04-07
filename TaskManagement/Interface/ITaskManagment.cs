using DocumentFormat.OpenXml.Office.Word;
using System.Threading.Tasks;
using TaskManagement.Models;

namespace TaskManagement.Interface
{
    public interface ITaskManagment
    {
        //Task<IEnumerable<Task>> Get();
        Task<IEnumerable<TaskManagement.Models.Task>> Get();
       // Task GetTaskByID(int id);
        int NewTask(Models.Task _model);
        Task<int> UpdateTask(Models.Task _model);
        bool DeleteTask(int id);
        //Task GetTaskByID(int id);
        Task<TaskManagement.Models.Task> GetTaskByID(int id);
    }
}
