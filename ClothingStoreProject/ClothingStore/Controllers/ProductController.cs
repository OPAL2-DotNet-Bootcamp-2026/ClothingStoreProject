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

        [HttpGet("GetProductsByGender")]
        public IActionResult GetProductsByGender([FromQuery] Gender gender)
        {
            List<ProductListItemDto> products = productService.GetProductsByGender(gender);

            return Ok(products);
        }

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

        [HttpGet("GetAvailableProducts")]
        public IActionResult GetAvailableProducts()
        {
            List<ProductListItemDto> products = productService.GetAvailableProducts();

            return Ok(products);
        }

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
    }
}
