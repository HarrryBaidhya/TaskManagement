using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.Models
{
    public class ProductModel
    {

        public int ProductID { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductName { get; set; } = string.Empty;

        [StringLength(50)]
        public string? Category { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        // Foreign key relationship
       // [ForeignKey("Supplier")]
        public int? SupplierID { get; set; }

        public bool IsActive { get; set; } = true;

        public string CreatedDate { get; set; } 
        // Navigation property
        public Supplier? Supplier { get; set; }
    }
}
