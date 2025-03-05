using Dapper;
using TaskManagement.ORMDapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.Data.SqlClient;
using System.Configuration;
using DocumentFormat.OpenXml.Office.Word;
using DocumentFormat.OpenXml.Office2010.Excel;

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

        public int AddRemit(Models.MoneyModel.ReciveDetails model)
        {

            //string query = "INSERT INTO dbo.TransactionABC (SenderFirstName, SenderLastname,SenderCountry,SenderAddress,RecieverFirstName,RecieverLastname,RecieverCountry,RecieverAddress,BankName,AccountNumber,TransferAmount,Exchangerate,Payamount,DateAbc) VALUES (@SenderFirstName, @SenderLastname, @SenderCountry,@SenderAddress, @RecieverFirstName, @RecieverLastname,RecieverCountry,RecieverAddress,@BankName,@AccountNumber,@TransferAmount,@Exchangerate,@Payamount,GETDATE())";
            // SQL query
            var sql = @"
        INSERT INTO dbo.TransactionABC (
            SenderFirstName, 
            SenderLastName, 
            SenderCountry, 
            SenderAddress, 
            RecieverFirstName, 
            RecieverLastName, 
            RecieverCountry, 
            RecieverAddress, 
            BankName, 
            AccountNumber, 
            TransferAmount, 
            Exchangerate, 
            Payamount, 
            DateAbc
        ) 
        VALUES (
            @SenderFirstName, 
            @SenderLastName, 
            @SenderCountry, 
            @SenderAddress, 
            @RecieverFirstName, 
            @RecieverLastName, 
            @RecieverCountry, 
            @RecieverAddress, 
            @BankName, 
            @AccountNumber, 
            @TransferAmount, 
            @Exchangerate, 
            @Payamount, 
            @DateAbc
        );";

            // Parameters
            var parameters = new
            {
                SenderFirstName = model.SenderName,
                SenderLastName = model.SenderLastName,
                SenderCountry = model.SenderCountry,
                SenderAddress = model.Address,
                RecieverFirstName = model.RecieverFirstName,
                RecieverLastName = model.RecieverLastname,
                RecieverCountry = model.Country,
                RecieverAddress = model.Address,
                BankName = model.BankName,
                AccountNumber = model.AccountNumber,
                TransferAmount = model.TransferAmount,
                Exchangerate = model.ExchangeRate,
                Payamount = model.Payamount,
                DateAbc = DateTime.UtcNow // Use current date/time
            };

            using (var connection = context.CreateConnection())

            {
                var Games = connection.QueryAsync<Task>(sql);
                return connection.Execute(sql, parameters);

            }
        }
        public int CreateRegister(Models.UserRegModel _model)
        {
            string query = "INSERT INTO dbo.ABCUsers(UserName,Password,Email,PhoneNumber,CreatedDate,Firstname,Lastname,Country,Address) VALUES (@UserName, @Password, @Email,@PhoneNumber, getdate(),@Firstname,@Lastname,@Country,@Address)";
            using (var connection = context.CreateConnection())

            {
                var Games = connection.QueryAsync<Task>(query);
                return connection.Execute(query, _model);
            }
        }

        public async Task<IEnumerable<Models.UserRegModel>> CheckUser(Models.UserRegModel _model)
        {
            try
            {
                var sql = "select * from ABCUsers where USERNAME='" + _model.UserName + "'";
                using var connection = context.CreateConnection();
                return await connection.QueryAsync<Models.UserRegModel>(sql);
            }
            catch (Exception ex)
            {
                 var sql = "select * from ABCUsers where USERNAME='" + _model.UserName + "'";
                using var connection = context.CreateConnection();
                return await connection.QueryAsync<Models.UserRegModel>(sql);
            }
        }

        public async Task<IEnumerable<Models.MoneyModel.ReciveDetails>> GetReport()
        {

                var sql = "select * from TransactionABC";
                using var connection = context.CreateConnection();
                return await connection.QueryAsync<Models.MoneyModel.ReciveDetails>(sql);
            
        }

        public async Task<IEnumerable<Models.UserRegModel>> GetUser()
        {
            var sql = "SELECT * FROM dbo.ABCUsers";
            using var connection = context.CreateConnection();
            return await connection.QueryAsync<Models.UserRegModel>(sql);
        }
    }
}
