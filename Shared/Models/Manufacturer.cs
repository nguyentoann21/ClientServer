using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
    public class Manufacturer
    {
        [Key]
        [StringLength(100, ErrorMessage = "Enter limit 100 characters")]
        public string ManufacturerID { get; set; } = string.Empty;
        [Required]
        [StringLength(100, ErrorMessage = "Enter limit 100 characters")]
        public string ManufacturerName { get; set; } = string.Empty;
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
