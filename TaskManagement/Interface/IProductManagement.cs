using DocumentFormat.OpenXml.Drawing.Charts;

namespace TaskManagement.Interface
{
    public interface IProductManagement
    {
        int NewSupplier(Models.Supplier _model);
        Task<IEnumerable<Models.Supplier>> Get();
        Task<IEnumerable<Models.ProductModel>> GetProduct(DateTime? searchDate, int pageNumber, int pageSize);
        int NewProduct(Models.ProductModel _model);
        Task<IEnumerable<Models.OrderModel>> GetOrder(DateTime? searchDate, int pageNumber, int pageSize);
        int NewOrder(Models.OrderModel _model);
        Task<IEnumerable<Models.OrderModel>> GetOrderById(int OrderId);
        int UpdateOrder(Models.OrderModel _model);
    }
}
