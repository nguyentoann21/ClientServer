using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Server;
using Shared.Dtos;
using Shared.Models;
using Shared.PageView;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Client.Controllers
{
    public class ProductsController : Controller
    {
        private readonly HttpClient httpClient;

        public ProductsController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://localhost:7186/api/");
        }

        public async Task<IActionResult> Index()
        {
            var res = await httpClient.GetStringAsync("products/GetProducts");
            var productDTO = JsonConvert.DeserializeObject<List<Product>>(res);
            var model = new ProductPageView
            {
                Products = productDTO,
                PageNumber = 1,
                PageSize = 10,
                TotalProducts = await GetTotalProducts()
            };

            return View(model);
        }

        public async Task<IActionResult> Page(int pageNumber)
        {
            var response = await httpClient.GetStringAsync($"products/GetProducts?pageNumber={pageNumber}&pageSize=10");
            var productDTO = JsonConvert.DeserializeObject<List<Product>>(response);
            var model = new ProductPageView
            {
                Products = productDTO,
                PageNumber = pageNumber,
                PageSize = 10,
                TotalProducts = await GetTotalProducts()
            };

            return View(model);
        }

        private async Task<int> GetTotalProducts()
        {
            var response = await httpClient.GetStringAsync("products/GetProducts");
            var productDTO = JsonConvert.DeserializeObject<List<Product>>(response);
            return productDTO.Count;
        }

        public async Task<IActionResult> Details(string id)
        {
            var res = await httpClient.GetStringAsync("products/GetSingleByID?id=" + id);
            var productDTO = JsonConvert.DeserializeObject<Product>(res);
            return View(productDTO);
        }

        [HttpGet("id")]
        public async Task<IActionResult> ProductBy(string id)
        {
            var res = await httpClient.GetStringAsync("products/GetProductByManufacturer?manufacturer=" + id);
            var productDTO = JsonConvert.DeserializeObject<List<Product>>(res);
            var view = new ProductByView
            {
                Products = productDTO
            };
            ViewBag.Total = "The (" + id + ") have " + productDTO.Count + " items(s)"; 
            return View(view);
        }

        public async Task<IActionResult> SearchByName(string name)
        {
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
            var content = new MultipartFormDataContent();

            content.Add(new StringContent(productDto.ProductID), "ProductID");
            content.Add(new StringContent(productDto.ProductName), "ProductName");
            content.Add(new StringContent(productDto.ProductDescription), "ProductDescription");
            content.Add(new StringContent(productDto.ProductPrice.ToString()), "ProductPrice");
            content.Add(new StringContent(productDto.Quantity.ToString()), "Quantity");
            content.Add(new StringContent(productDto.CreateOn.ToString("dd/MM/yyyy")), "CreateOn");
            content.Add(new StreamContent(productDto.ProductImage.OpenReadStream()), "ProductImage", productDto.ProductImage.FileName);
            content.Add(new StringContent(productDto.ManufacturerID), "ManufacturerID");

            var response = await httpClient.PostAsync(url, content);
            var responseContent = await response.Content.ReadAsStringAsync();
            //var result = JsonConvert.DeserializeObject<CreateProductDto>(responseContent);

            return RedirectToAction("Index", "Products");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var resp = await httpClient.GetStringAsync("manufacturers/GetCategories");
            var manufacturers = JsonConvert.DeserializeObject<List<ManufacturerDto>>(resp);
            ViewBag.Manufacturer = new SelectList(manufacturers, "ManufacturerID", "ManufacturerName");
            var res = await httpClient.GetStringAsync("products/GetSingleByID?id=" + id);
            var productDto = JsonConvert.DeserializeObject<Product>(res);
            return View(productDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CreateProductDto productDto)
        {
            var url = "https://localhost:7186/api/products/Update";
            var content = new MultipartFormDataContent();

            content.Add(new StringContent(productDto.ProductID), "ProductID");
            content.Add(new StringContent(productDto.ProductName), "ProductName");
            content.Add(new StringContent(productDto.ProductDescription), "ProductDescription");
            content.Add(new StringContent(productDto.ProductPrice.ToString()), "ProductPrice");
            content.Add(new StringContent(productDto.Quantity.ToString()), "Quantity");
            content.Add(new StringContent(productDto.CreateOn.ToString("dd/MM/yyyy")), "CreateOn");
            content.Add(new StreamContent(productDto.ProductImage.OpenReadStream()), "ProductImage", productDto.ProductImage.FileName);
            content.Add(new StringContent(productDto.ManufacturerID), "ManufacturerID");

            var response = await httpClient.PutAsync(url, content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CreateProductDto>(responseContent);

            return RedirectToAction("Index", "Products");
        }
    }
}