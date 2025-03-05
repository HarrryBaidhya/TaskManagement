using DocumentFormat.OpenXml.Office.Word;
using TaskManagement.Models;

namespace TaskManagement.Interface
{
    public interface ITaskManagment
    {
        //Task<IEnumerable<Task>> Get();
        Task<IEnumerable<TaskManagement.Models.Task>> Get();
       // Task GetTaskByID(int id);
        int NewTask(Models.Task _model);
       // Task<IEnumerable<Models.Task>> GetTaskByID(int id);
        // bool UpdateTask(Models.Task _model);
        Task<int> UpdateTask(Models.Task _model);
        bool DeleteTask(int id);
       // Task GetTaskByID(int id);

        int AddRemit(Models.MoneyModel.ReciveDetails _model);

        int CreateRegister(Models.UserRegModel _model);

      // bool CheckUser(Models.UserRegModel _model);


        Task<IEnumerable<Models.UserRegModel>> CheckUser(Models.UserRegModel model);
    }
}
