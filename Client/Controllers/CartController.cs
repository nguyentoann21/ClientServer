using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.Interfaces;
using Shared.Models;
using Shared.PageView;
using Shared.Services;
using System.Net.Http;
using System.Text;

namespace Client.Controllers
{
    public class CartController : Controller
    {
        HttpClient httpClient;

        public CartController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7186/api/");
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(string productId, int quantity)
        {
            var product = await GetProduct(productId);

            if (product == null)
            {
                return NotFound();
            }

            var content = new StringContent(JsonConvert.SerializeObject(new { productId, quantity }), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("cart", content);

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest();
            }

            return RedirectToAction("Index", "Cart");
        }

        private async Task<Product> GetProduct(string productId)
        {
            var response = await httpClient.GetAsync($"products/GetSingleByID/id={productId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<Product>(json);

            return product;
        }

    }
}