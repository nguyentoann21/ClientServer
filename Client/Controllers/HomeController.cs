using Client.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shared.Models;
using Shared.PageView;
using System.Diagnostics;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient httpClient;

        public HomeController()
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7186/api/")
            };
        }

        public async Task<IActionResult> Index()
        {
            var res = await httpClient.GetStringAsync("products/GetProducts");
            var productDTO = JsonConvert.DeserializeObject<List<Product>>(res);
            var model = new ProductPageView
            {
                Products = productDTO
            };

            return View(model);
        }

        [HttpGet("id")]
        public async Task<IActionResult> Categories(string id)
        {
            var res = await httpClient.GetStringAsync("products/GetProductByManufacturer?manufacturer=" + id);
            var productDTO = JsonConvert.DeserializeObject<List<Product>>(res);
            var view = new ProductByView
            {
                Products = productDTO
            };
            ViewBag.Total = "This manufacturer have " + productDTO.Count + " items(s)";
            return View(view);
        }

        public async Task<IActionResult> SearchByName(string name)
        {
            if (name == null)
            {
                var res = await httpClient.GetStringAsync("products/GetProducts");
                var rs = JsonConvert.DeserializeObject<List<Product>>(res);

                var products = new ProductByView
                {
                    Products = rs
                };
                ViewBag.Message = "Found " + rs.Count + " item(s)";
                return View(products);

            }
            var response = await httpClient.GetStringAsync("products/SearchByName/name?name=" + name);
            var result = JsonConvert.DeserializeObject<List<Product>>(response);

            var view = new ProductByView
            {
                Products = result
            };
            ViewBag.Message = "Found " + result.Count + " item(s)";
            return View(view);
        }

        public async Task<IActionResult> Details(string id)
        {
            var res = await httpClient.GetStringAsync("products/GetSingleByID?id=" + id);
            var productDTO = JsonConvert.DeserializeObject<Product>(res);
            return View(productDTO);
        }
    }
}