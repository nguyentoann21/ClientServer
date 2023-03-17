using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Models
{
    public class Product
    {
        [Key]
        [StringLength(100, ErrorMessage = "Enter limit 100 characters")]
        public string ProductID { get; set; } = string.Empty;
        [Required]
        [StringLength(100, ErrorMessage = "Enter limit 100 characters")]
        public string ProductName { get; set; } = string.Empty;
        [Required]
        [StringLength(200, ErrorMessage = "Enter limit 200 characters")]
        public string ProductDescription { get; set; } = string.Empty;
        [Required]
        public Decimal ProductPrice { get; set; }
        [Required]
        public Int32 Quantity { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:đd/MM/yyyy}")]
        public DateTime CreateOn { get; set; }
        public string ProductImage { get; set; } = string.Empty;

        [ForeignKey(nameof(Manufacturer))]
        public string ManufacturerID { get; set; } = string.Empty;
        public Manufacturer Manufacturers { get; set; } = null!;
    }
}
