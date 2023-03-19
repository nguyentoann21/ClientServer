using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Shared.Dtos;
using Shared.Models;
using Shared.PageView;

namespace Client.Controllers
{
    public class AdminController : Controller
    {
        private readonly HttpClient httpClient;

        public AdminController()
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

        public async Task<IActionResult> Create()
        {
            var response = await httpClient.GetStringAsync("manufacturers/GetCategories");
            var manufacturers = JsonConvert.DeserializeObject<List<ManufacturerDto>>(response);
            ViewBag.Manufacturer = new SelectList(manufacturers, "ManufacturerID", "ManufacturerName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductDto productDto)
        {
            var url = "https://localhost:7186/api/products/Create";
            var content = new MultipartFormDataContent
            {
                { new StringContent(productDto.ProductName), "ProductName" },
                { new StringContent(productDto.ProductDescription), "ProductDescription" },
                { new StringContent(productDto.ProductPrice.ToString()), "ProductPrice" },
                { new StringContent(productDto.Quantity.ToString()), "Quantity" },
                { new StringContent(productDto.CreateOn.ToString("dd/MM/yyyy")), "CreateOn" },
                { new StreamContent(productDto.ProductImage.OpenReadStream()), "ProductImage", productDto.ProductImage.FileName },
                { new StringContent(productDto.ManufacturerID), "ManufacturerID" }
            };

            var response = await httpClient.PostAsync(url, content);
            await response.Content.ReadAsStringAsync();
            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var resp = await httpClient.GetStringAsync("manufacturers/GetCategories");
            var manufacturers = JsonConvert.DeserializeObject<List<ManufacturerDto>>(resp);
            ViewBag.Manufacturer = new SelectList(manufacturers, "ManufacturerID", "ManufacturerName");
            var res = await httpClient.GetStringAsync("products/GetSingleByID?id=" + id);
            var productDto = JsonConvert.DeserializeObject<Product>(res);
            return View(productDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateProductDto productDto)
        {
            var url = "https://localhost:7186/api/products/Update";
            var content = new MultipartFormDataContent
            {
                { new StringContent(productDto.ProductID.ToString()), "ProductID" },
                { new StringContent(productDto.ProductName), "ProductName" },
                { new StringContent(productDto.ProductDescription), "ProductDescription" },
                { new StringContent(productDto.ProductPrice.ToString()), "ProductPrice" },
                { new StringContent(productDto.Quantity.ToString()), "Quantity" },
                { new StringContent(productDto.CreateOn.ToString("dd/MM/yyyy")), "CreateOn" },
                { new StreamContent(productDto.ProductImage.OpenReadStream()), "ProductImage", productDto.ProductImage.FileName },
                { new StringContent(productDto.ManufacturerID), "ManufacturerID" }
            };

            var response = await httpClient.PutAsync(url, content);
            await response.Content.ReadAsStringAsync();
            return RedirectToAction("Index", "Admin");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var res = await httpClient.GetStringAsync("products/GetSingleByID?id=" + id);
            var productDTO = JsonConvert.DeserializeObject<Product>(res);
            return View(productDTO);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await httpClient.DeleteAsync("products/Delete?id=" + id);
            return RedirectToAction(nameof(Index));
        }
    }
}
