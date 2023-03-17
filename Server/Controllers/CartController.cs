using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Interfaces;
using Shared.Models;
using Shared.PageView;
using Shared.Services;

namespace Server.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartServices _cartService;
        private readonly IProductServices _productService;

        public CartController(ICartServices cartService, IProductServices productService)
        {
            _cartService = cartService;
            _productService = productService;
        }

        [HttpPost]
        public IActionResult AddToCart(string productId, int quantity)
        {
            var product = _productService.GetByID(productId);

            if (product == null)
            {
                return NotFound();
            }

            _cartService.AddItem(product, quantity);

            return Ok(product);
        }

        [HttpGet]
        public IActionResult GetCart()
        {
            var items = _cartService.GetItems();
            var cartItems = new List<CartItem>();

            foreach (var item in items)
            {
                cartItems.Add(new CartItem
                {
                    Product = item.Product,
                    Quantity = item.Quantity
                });
            }

            var cartViewModel = new CartViewModel
            {
                CartItems = cartItems,
                TotalPrice = _cartService.GetTotal()
            };

            return Ok(cartViewModel);
        }
    }
}
