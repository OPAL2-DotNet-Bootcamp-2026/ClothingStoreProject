using ClothingStore.DTOs;
using ClothingStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.Controllers
{
    [Route("brand")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private BrandService service;

        public BrandController(BrandService _service)
        {
            service = _service;
        }

        [HttpGet("GetAllBrands")]
        public IActionResult GetAllBrands()
        {
            var brands = service.GetAllBrands();
            return Ok(brands);
        }


        [HttpGet("GetBrandById")]
        public IActionResult GetBrandById([FromQuery] int id)
        {
            var brand = service.GetBrandById(id);

            if (brand == null)
                return NotFound("Brand not found.");

            return Ok(brand);
        }

        
        [HttpPost("AddBrand")]
        public IActionResult AddBrand([FromBody] CreateBrandDto dto)
        {
            var brand = service.AddBrand(dto);

            if (brand == null)
                return BadRequest("A brand with this name already exists.");

            return CreatedAtAction(nameof(GetBrandById), new { id = brand.brandId }, brand);
        }

        [HttpPut("UpdateBrand")]
        public IActionResult UpdateBrand([FromQuery] int id, [FromBody] UpdateBrandDto dto)
        {
            var brand = service.UpdateBrand(id, dto);

            if (brand == null)
                return BadRequest("Brand not found or the requested name is already in use.");

            return Ok(brand);
        }

        [HttpDelete("DeactivateBrand")]
        public IActionResult DeactivateBrand([FromQuery] int id)
        {
            var deactivated = service.DeactivateBrand(id);

            if (!deactivated)
                return NotFound("Brand not found.");

            return NoContent();
        }

        [HttpGet("GetProductsByBrandId")]
        public IActionResult GetProductsByBrandId([FromQuery] int brandId)
        {
            var products = service.GetProductsByBrandId(brandId);

            if (products == null)
                return NotFound("Brand not found.");

            return Ok(products);
        }
    }
}
