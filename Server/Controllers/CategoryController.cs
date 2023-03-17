using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Shared.Interfaces;
using Shared.Models;

namespace Server.Controllers
{
    [Route("api/manufacturers/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICaregoryServices _services;

        public CategoryController(ICaregoryServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _services.GetAll();
            return Ok(categories);
        }

        [HttpGet]
        public async Task<IActionResult> GetSingleByID(string? id)
        {
            var category = await _services.GetSingleByID(id);
            if (category == null)
            {
                return NotFound("Manufacturer does not exist");
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ManufacturerDto manufacturer)
        {
            var check = await _services.GetSingleByID(manufacturer.ManufacturerID);
            if (check != null)
            {
                return NotFound("The manufacturer is already exist");
            }
            var category = new Manufacturer
            {
                ManufacturerID = manufacturer.ManufacturerID,
                ManufacturerName = manufacturer.ManufacturerName
            };
            await _services.Create(category);
            return Ok(category);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ManufacturerDto manufacturer)
        {
            var check = await _services.GetSingleByID(manufacturer.ManufacturerID);
            if (check == null)
            {
                return NotFound("The manufacturer does not exist");
            }
            var category = new Manufacturer
            {
                ManufacturerID = manufacturer.ManufacturerID,
                ManufacturerName = manufacturer.ManufacturerName
            };
            await _services.Update(category);
            return Ok(category);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var category = await _services.GetSingleByID(id);
            if (category == null)
            {
                return NotFound("The manufacturer does not found");
            }
            await _services.Delete(category);
            return StatusCode(StatusCodes.Status202Accepted, "Deleted");
        }
    }
}
