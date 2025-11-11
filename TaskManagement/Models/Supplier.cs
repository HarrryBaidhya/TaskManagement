using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public class Supplier
    {
        public int SupplierID { get; set; }

        [Required]
        public string SupplierName { get; set; } = string.Empty;

        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? City { get; set; }

        [Required]
        public string? Country { get; set; }
    }
}
