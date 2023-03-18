using Microsoft.AspNetCore.Http;
using Shared.Models;

namespace Shared.Dtos
{
    public class ProductDto
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public Decimal ProductPrice { get; set; }
        public Int32 Quantity { get; set; }
        public DateTime CreateOn { get; set; }
        public IFormFile ProductImage { get; set; }
        public string ManufacturerID { get; set; }
        public string ManufacturerName { get; set; }
    }
}
