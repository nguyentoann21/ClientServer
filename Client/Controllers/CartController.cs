using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.Models;
using System.Net;
using System.Net.Http;

namespace Client.Controllers
{
    public class CartController : Controller
    {
        [Route("Cart/AddToCart/{id}/{quantity}")]
        [HttpPost]
        public async Task<ActionResult> AddToCart(string productId, int quantity)
        {
            
            var client = new HttpClient();

            var response = await client.PostAsync($"https://localhost:7186/api/carts/AddToCart?productId="+productId+"&quantity="+quantity, null);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var cartItems = JsonConvert.DeserializeObject<List<Product>>(content);
                return View("Index", cartItems);
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return View("Error", errorMessage);
            }
        }




        public async Task<ActionResult> Index()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("https://localhost:7186/api/carts/Get");

            if (response.IsSuccessStatusCode)
            {
                var products = await response.Content.ReadAsAsync<List<Product>>();
                return View(products);
            }
            else
            {
                return View("Error");
            }
        }

        [Route("{productId}")]
        [HttpPost]
        public async Task<ActionResult> RemoveProduct(string productId)
        {
            using (var client = new HttpClient())
            {
                var url = $"https://localhost:7186/api/carts/RemoveProduct?productId={productId}";
                var response = await client.DeleteAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var products = await response.Content.ReadAsAsync<List<Product>>();
                    return View("Index", products);
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    ViewBag.ErrorMessage = "Product not found in cart";
                    return View("Error");
                }
                else
                {
                    ViewBag.ErrorMessage = "Error removing product from cart";
                    return View("Error");
                }
            }
        }

    }
}
