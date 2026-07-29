using ClothingStore.DTOs;
using ClothingStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
namespace ClothingStore.Controllers
{
    [Authorize]
    [EnableRateLimiting("public")]
    [Route("category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private CategoryService service;

        public CategoryController(CategoryService _service)
        {
            service = _service;
        }

        [AllowAnonymous]
        [HttpGet("GetAllCategories")]
        public IActionResult GetAllCategories()
        {
            var categories = service.GetAllCategories();
            return Ok(categories);
        }

        [AllowAnonymous]
        [HttpGet("GetCategoryById")]
        public IActionResult GetCategoryById([FromQuery] int id)
        {
            var category = service.GetCategoryById(id);

            if (category == null)
                return NotFound("Category not found.");

            return Ok(category);
        }

        [AllowAnonymous]
        [HttpGet("GetTopLevelCategories")]
        public IActionResult GetTopLevelCategories()
        {
            var categories = service.GetTopLevelCategories();
            return Ok(categories);
        }

        [AllowAnonymous]
        [HttpGet("GetSubcategories")]
        public IActionResult GetSubcategories([FromQuery] int parentId)
        {
            var categories = service.GetSubcategories(parentId);

            if (categories == null)
                return NotFound("Parent category not found.");

            return Ok(categories);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AddCategory")]
        public IActionResult AddCategory([FromBody] CreateCategoryDto dto)
        {
            var category = service.AddCategory(dto);

            if (category == null)
                return BadRequest("A category with this name already exists, or the parent category does not exist.");

            return CreatedAtAction(nameof(GetCategoryById), new { id = category.categoryId }, category);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateCategory")]
        public IActionResult UpdateCategory([FromQuery] int id, [FromBody] UpdateCategoryDto dto)
        {
            var category = service.UpdateCategory(id, dto);

            if (category == null)
                return BadRequest("Category not found, name already in use, or the parent category is invalid or would create a cycle.");

            return Ok(category);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("DeactivateCategory")]
        public IActionResult DeactivateCategory([FromQuery] int id)
        {
            var result = service.DeactivateCategory(id);

            if (result == null)
                return NotFound("Category not found.");

            if (result == false)
                return Conflict("Category still has active products and cannot be deactivated.");

            return NoContent();
        }

        
        //[HttpGet("GetProductsByCategoryId")]
        //public IActionResult GetProductsByCategoryId([FromQuery] int categoryId)
        //{
        //    var products = service.GetProductsByCategoryId(categoryId);

        //    if (products == null)
        //        return NotFound("Category not found.");

        //    return Ok(products);
        //}
    }
}
