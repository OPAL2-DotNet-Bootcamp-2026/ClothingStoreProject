using ClothingStore.DTOs;
using ClothingStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.Controllers
{
    [ApiController]
    [Route("variant")]
    public class ProductVariantController : ControllerBase
    {
        private readonly ProductVariantService productVariantService;

        public ProductVariantController(
            ProductVariantService productVariantService)
        {
            this.productVariantService = productVariantService;
        }

        [HttpGet("GetAllVariants")]
        public IActionResult GetAllVariants()
        {
            List<VariantResponseDto> variants = productVariantService.GetAllVariants();

            return Ok(variants);
        }

        [HttpGet("GetVariantById")]
        public IActionResult GetVariantById([FromQuery] int id)
        {
            if (id <= 0)
            {
                return BadRequest("Variant Id must be greater than 0.");
            }

            VariantResponseDto? variant =
                productVariantService.GetVariantById(id);

            if (variant == null)
            {
                return NotFound("Variant was not found.");
            }

            return Ok(variant);
        }

        [HttpGet("GetVariantsByProduct")]
        public IActionResult GetVariantsByProduct([FromQuery] int productId)
        {
            if (productId <= 0)
            {
                return BadRequest("Product Id must be greater than 0.");
            }

            List<VariantResponseDto> variants =
                productVariantService.GetVariantsByProduct(productId);

            return Ok(variants);
        }
    }
}
