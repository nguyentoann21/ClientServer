using Microsoft.AspNetCore.Http;
using Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos
{
    public class CreateProductDto
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public Decimal ProductPrice { get; set; }
        public Int32 Quantity { get; set; }
        [DisplayFormat(DataFormatString = "{0:đd/MM/yyyy}")]
        public DateTime CreateOn { get; set; }
        public IFormFile ProductImage { get; set; }
        public string ManufacturerID { get; set; }
    }
}
