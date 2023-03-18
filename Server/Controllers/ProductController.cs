using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Shared.Interfaces;
using Shared.Models;

namespace Server.Controllers
{
    [Route("api/products/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _services;

        public ProductController(IProductServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _services.GetAll(1, 10);            
            return Ok(products);
        }

        [HttpGet]
        public async Task<IActionResult> GetSingleByID(int id)
        {
            var product = await _services.GetSingleByID(id);
            if (product == null)
            {
                return NotFound("Product does not exist");
            }
            return Ok(product);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateProductDto productDto)
        {
            var check = await _services.GetSingleByID(productDto.ProductID);
            if (check != null)
            {
                return NotFound("The product is already exist");
            }
            var product = new Product
            {
                ProductName = productDto.ProductName,
                ProductDescription = productDto.ProductDescription,
                ProductPrice = productDto.ProductPrice,
                Quantity = productDto.Quantity,
                CreateOn = productDto.CreateOn,
                ProductImage = await Utils.Upload(productDto.ProductImage),
                ManufacturerID = productDto.ManufacturerID,
            };
            await _services.Create(product);
            return Ok(productDto);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] CreateProductDto productDto)
        {
            var check = await _services.GetSingleByID(productDto.ProductID);
            if (check == null)
            {
                return NotFound("The product does not exist");
            }
            var product = new Product
            {
                ProductID = productDto.ProductID,
                ProductName = productDto.ProductName,
                ProductDescription = productDto.ProductDescription,
                ProductPrice = productDto.ProductPrice,
                Quantity = productDto.Quantity,
                CreateOn = productDto.CreateOn,
                ProductImage = await Utils.Upload(productDto.ProductImage),
                ManufacturerID = productDto.ManufacturerID,
            };
            await _services.Update(product);
            return Ok(product);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _services.GetSingleByID(id);
            if (category == null)
            {
                return NotFound("The product does not found");
            }
            await _services.Delete(category);
            return StatusCode(StatusCodes.Status202Accepted, "Deleted");
        }

        [HttpGet]
        public async Task<IActionResult> GetProductByManufacturer(string manufacturer)
        {
            var products = await _services.GetProductByManufacturer(manufacturer);

            if (products == null || products.Count() == 0)
            {
                return NotFound("The manufacturer does not found any product");
            }
            return Ok(products);
        }

        [HttpGet("name")]
        public async Task<IActionResult> SearchByName(string name)
        {
            var result = await _services.SearchByName(name);
            return Ok(result);
        }
    }
}
