namespace TaskManagement.Interface
{
    public interface ITaskManagment
    {
        Task<IEnumerable<Models.Task>> Get();
        int NewTask(Models.Task _model);
        Task<int> UpdateTask(Models.Task _model);
        bool DeleteTask(int id);
        Task<Models.Task> GetTaskByID(int id);


        // Task GetTaskByID(int id);
        //Task<IEnumerable<Task>> Get();
    }
}