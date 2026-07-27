using ClothingStore.DTOs;
using ClothingStore.Services;
using Microsoft.AspNetCore.Mvc;
using static ClothingStore.Enums;

namespace ClothingStore.Controllers
{
    [ApiController]
    [Route("product")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService productService;

        public ProductController(ProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet("GetAllProducts")]
        public IActionResult GetAllProducts()
        {
            List<ProductListItemDto> products = productService.GetAllProducts();

            return Ok(products);
        }

        [HttpGet("GetProductById")]
        public IActionResult GetProductById([FromQuery] int id)
        {
            if (id <= 0)
            {
                return BadRequest("Product Id must be greater than 0.");
            }

            ProductDetailDto? product = productService.GetProductById(id);

            if (product == null)
            {
                return NotFound("Product was not found.");
            }

            return Ok(product);
        }

        [HttpGet("GetProductsByBrand")]
        public IActionResult GetProductsByBrand([FromQuery] int brandId)
        {
            if (brandId <= 0)
            {
                return BadRequest("Brand Id must be greater than 0.");
            }

            List<ProductListItemDto> products = productService.GetProductsByBrand(brandId);

            return Ok(products);
        }
    }
}
