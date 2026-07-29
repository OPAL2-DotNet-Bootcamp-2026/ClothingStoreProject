using ClothingStore.DTOs;
using ClothingStore.Services;
using Microsoft.AspNetCore.Mvc;
using static ClothingStore.Enums;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Authorization;
namespace ClothingStore.Controllers
{
    [Authorize]
    [EnableRateLimiting("public")]
    [ApiController]
    [Route("product")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService productService;

        public ProductController(ProductService productService)
        {
            this.productService = productService;
        }

        [AllowAnonymous]
        [HttpGet("GetAllProducts")]
        public IActionResult GetAllProducts()
        {
            List<ProductListItemDto> products = productService.GetAllProducts();

            return Ok(products);
        }

        [AllowAnonymous]
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

        [AllowAnonymous]
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

        [AllowAnonymous]
        [HttpGet("GetProductsByCategory")]
        public IActionResult GetProductsByCategory([FromQuery] int categoryId)
        {
            if (categoryId <= 0)
            {
                return BadRequest("Category Id must be greater than 0.");
            }

            List<ProductListItemDto> products = productService.GetProductsByCategory(categoryId);

            return Ok(products);
        }

        [AllowAnonymous]
        [HttpGet("GetProductsByGender")]
        public IActionResult GetProductsByGender([FromQuery] Gender gender)
        {
            List<ProductListItemDto> products = productService.GetProductsByGender(gender);

            return Ok(products);
        }

        [AllowAnonymous]
        [HttpGet("SearchProductsByName")]
        public IActionResult SearchProductsByName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Product name is required.");
            }

            List<ProductListItemDto> products = productService.SearchProductsByName(name);

            return Ok(products);
        }
        [AllowAnonymous]
        [HttpGet("GetAvailableProducts")]
        public IActionResult GetAvailableProducts()
        {
            List<ProductListItemDto> products = productService.GetAvailableProducts();

            return Ok(products);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AddProduct")]
        public IActionResult AddProduct([FromBody] CreateProductDto dto)
        {
            ProductDetailDto? product = productService.AddProduct(dto);

            if (product == null)
            {
                return BadRequest(
                    "Product could not be added. " +
                    "The product name may already exist, " +
                    "or the Brand Id or Category Id may be invalid.");
            }

            return CreatedAtAction(
                nameof(GetProductById),
                new { id = product.productId },
                product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateProduct")]
        public IActionResult UpdateProduct([FromQuery] int id,[FromBody] UpdateProductDto dto)
        {
            if (id <= 0)
            {
                return BadRequest("Product Id must be greater than 0.");
            }

            ProductDetailDto? existingProduct = productService.GetProductById(id);

            if (existingProduct == null)
            {
                return NotFound("Product was not found.");
            }

            ProductDetailDto? updatedProduct = productService.UpdateProduct(id, dto);

            if (updatedProduct == null)
            {
                return BadRequest(
                    "Product could not be updated. " +
                    "The product name may already exist, " +
                    "or the Brand Id or Category Id may be invalid.");
            }

            return Ok(updatedProduct);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeactivateProduct")]
        public IActionResult DeactivateProduct([FromQuery] int id)
        {
            if (id <= 0)
            {
                return BadRequest("Product Id must be greater than 0.");
            }

            bool isDeactivated = productService.DeactivateProduct(id);

            if (!isDeactivated)
            {
                return NotFound("Product was not found.");
            }

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("ToggleAvailability")]
        public IActionResult ToggleAvailability([FromQuery] int id)
        {
            if (id <= 0)
            {
                return BadRequest("Product Id must be greater than 0.");
            }

            bool isToggled = productService.ToggleAvailability(id);

            if (!isToggled)
            {
                return NotFound("Product was not found.");
            }

            return NoContent();
        }
    }
}
