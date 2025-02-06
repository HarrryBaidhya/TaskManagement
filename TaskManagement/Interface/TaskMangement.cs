using Dapper;
using TaskManagement.ORMDapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace TaskManagement.Interface
{
    public class TaskMangement : ITaskManagment
    {
        private readonly DapperContext context;

        public TaskMangement(DapperContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Models.Task>> Get()
        {
            var sql = "SELECT * FROM Tasks";
            using var connection = context.CreateConnection();
            return await connection.QueryAsync<Models.Task>(sql);
        }
        public int NewTask(Models.Task _Ttask)
        {

            string query = "INSERT INTO Tasks (Title, Description, Status,CreatedAt,DueDate) VALUES (@Title, @Description, @status,GETDATE(),@dueDate)";
            using (var connection = context.CreateConnection())

            {
                var Games = connection.QueryAsync<Task>(query);
                return connection.Execute(query, _Ttask);

            }

        }
        
        public Task GetTaskByID(int id)
        {
            var sql = "SELECT * FROM Tasks where id='"+id+"'";

            using var connection = context.CreateConnection();
            return connection.QueryAsync<Models.Task>(sql, new { id });
        }


        public async Task<int> UpdateTask(Models.Task _model)
        {
            string query = "UPDATE Tasks SET Title ='" + _model.Title + "', Description = '" + _model.Description + "' , DueDate = '" + _model.DueDate + "' , Status = '" + _model.Status + "' where ID =" + Convert.ToString(_model.Id);

            using var connection = context.CreateConnection();
            //int rowsAffected = connection.Executeasync<int>(query, _model);
            //return rowsAffected > 0;
            return connection.Execute(query, new
            {
                Title = _model.Title,
                Description = _model.Description,
                DueDate = _model.DueDate,
                Status = _model.Status,
                Id = _model.Id
            });

        }
        public bool DeleteTask(int Id)
        {
            string query = "DELETE FROM Tasks WHERE ID = @Id";
            using var connection = context.CreateConnection();
            int rowsAffected = connection.Execute(query, new { Id });

            return rowsAffected > 0;
        }


    }
}
