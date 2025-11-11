namespace TaskManagement.Models
{
    public class OrderModel
    {

        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }

        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;

        public decimal TotalAmount { get; set; }
        public string OrderStatus { get; set; } = string.Empty;
        public int OrderDetailID { get; set; }

       // public int OrderID { get; set; }
        public int ProductID { get; set; }

        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }
    }


}
