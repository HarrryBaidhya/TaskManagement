using Dapper;
using System.Data;
using System.Text;
using TaskManagement.Models;
using TaskManagement.ORMDapper;

namespace TaskManagement.Interface
{
    public class ProductManagement : IProductManagement
    {

        private readonly DapperContext context;

        public ProductManagement(DapperContext context)
        {
            this.context = context;
        }

        public int NewSupplier(Supplier _model)
        {

            string query = "INSERT INTO Supplier (SupplierName, Email, City, Country) VALUES (@SupplierName, @Email, @City,@Country)";
            using (var connection = context.CreateConnection())

            {
                var Suprep = connection.QueryAsync<Supplier>(query);
                return connection.Execute(query, _model);

            }
        }


        public async Task<IEnumerable<Models.Supplier>> Get()
        {
            var sql = "SELECT * FROM Supplier";
            using var connection = context.CreateConnection();
            return await connection.QueryAsync<Models.Supplier>(sql);
        }


        public async Task<IEnumerable<ProductModel>> GetProduct(DateTime? searchDate, int pageNumber, int pageSize)
        {
            var sql = new StringBuilder("SELECT * FROM Product WHERE 1=1");

            if (searchDate.HasValue)
            {
                sql.Append(" AND CAST(CreatedDate AS DATE) = @searchDate");
            }

            sql.Append(" ORDER BY ProductID  desc OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;");

            using var connection = context.CreateConnection();
            return await connection.QueryAsync<ProductModel>(sql.ToString(), new
            {
                searchDate,
                Offset = (pageNumber - 1) * pageSize,
                PageSize = pageSize
            });
        }


        public int NewProduct(ProductModel _model)
        {

            string query = "INSERT INTO Product (ProductName, Category, Price, StockQuantity, SupplierID, IsActive, CreatedDate) VALUES (@ProductName, @Category,@Price,@StockQuantity,@SupplierID,@IsActive,getdate())";
            using (var connection = context.CreateConnection())

            {
                var Suprep = connection.QueryAsync<ProductModel>(query);
                return connection.Execute(query, _model);

            }
        }


        public async Task<IEnumerable<OrderModel>> GetOrder(DateTime? searchDate, int pageNumber, int pageSize)
        {
            var sql = new StringBuilder("SELECT * FROM dbo.[Order] WHERE 1=1");

            if (searchDate.HasValue)
            {
                sql.Append(" AND CAST(OrderDate AS DATE) = @searchDate");
            }

            sql.Append(" ORDER BY OrderId  desc OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;");

            using var connection = context.CreateConnection();
            return await connection.QueryAsync<OrderModel>(sql.ToString(), new
            {
                searchDate,
                Offset = (pageNumber - 1) * pageSize,
                PageSize = pageSize
            });
        }


        //public int NewOrder(OrderModel _model)
        //{

        //}
        public int NewOrder(OrderModel _model)
        {
            using (var connection = context.CreateConnection())
            {
                var parameters = new
                {
                    _model.CustomerName,
                    _model.CustomerEmail,
                    _model.OrderStatus,
                    _model.Quantity,
                    _model.ProductID,
                    _model.SubTotal
                };

                var result = connection.QueryAsync<int>(
                          "spInsertOrder",
                            parameters,
                       commandType: CommandType.StoredProcedure);

                var newId = result.Id;
                return newId;
            }

        }
        public async Task<IEnumerable<Models.OrderModel>> GetOrderById(int OrderID)
        {
            var sql = "select * from [Order] where OrderID='"+ OrderID + "'";
            using var connection = context.CreateConnection();
            return await connection.QueryAsync<Models.OrderModel>(sql);
        }


        public int UpdateOrder(OrderModel _model)
        {
            using (var connection = context.CreateConnection())
            {
                var parameters = new
                {
                    _model.CustomerName,
                    _model.CustomerEmail,
                    _model.OrderStatus,
                    _model.Quantity,
                    _model.ProductID,
                    _model.SubTotal,
                    _model.OrderID
                };

                var result = connection.QueryAsync<int>(
                          "spUpdateOrder",
                            parameters,
                       commandType: CommandType.StoredProcedure);

                var newId = result.Id;
                return newId;
            }

        }



    }
}