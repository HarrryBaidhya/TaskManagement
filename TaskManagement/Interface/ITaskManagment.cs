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
       // Task GetTaskByID(int id);



        int AddRemit(Models.MoneyModel.ReciveDetails _model);

        int CreateRegister(Models.UserRegModel _model);

        // bool CheckUser(Models.UserRegModel _model);

        Task<IEnumerable<Models.MoneyModel.ReciveDetails>> GetReport();
        Task<IEnumerable<Models.UserRegModel>> GetUser();
        Task<IEnumerable<Models.UserRegModel>> CheckUser(Models.UserRegModel model);
    }
}
