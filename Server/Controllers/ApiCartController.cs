using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Interfaces;
using Shared.Models;

namespace Server.Controllers
{
    [Route("api/carts/[action]")]
    [ApiController]
    public class ApiCartController : ControllerBase
    {
        private static readonly List<Product> _cart = new();
        private readonly IProductServices _services;

        public ApiCartController(IProductServices services)
        {
            _services = services;
        }

        [HttpGet]
        public ActionResult<List<Product>> Get()
        {
            return Ok(_cart);
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetById(int id)
        {
            var product = _cart.FirstOrDefault(p => p.ProductID == id);
            return Ok(product);
        }

        [HttpPost]
        public ActionResult<List<Product>> AddToCart(int productId, int quantity)
        {
            var productDto = _services.GetByID(productId);
            var product = new Product
            {
                ProductID = productDto.ProductID,
                ProductName = productDto.ProductName,
                ProductDescription = productDto.ProductDescription,
                ProductPrice = productDto.ProductPrice,
                CreateOn = DateTime.Now,
                Quantity = quantity,
                ProductImage = productDto.ProductImage,
                ManufacturerID = productDto.ManufacturerID
            };

            if (product != null)
            {
                if (productDto.Quantity >= quantity)
                {
                    _cart.Add(product);
                    return Ok(_cart);
                }
                else
                {
                    return NotFound("No have enough item(s)");
                }
            }
            else
            {
                return BadRequest("Invalid product id");
            }
        }


        [HttpDelete]
        public ActionResult<List<Product>> RemoveProduct(int productId)
        {
            var productToRemove = _cart.FirstOrDefault(p => p.ProductID == productId);

            if (productToRemove != null)
            {
                _cart.Remove(productToRemove);
                return Ok(_cart);
            }
            else
            {
                return BadRequest("Product not found in cart");
            }
        }
    }
}
