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

            List<VariantResponseDto> variants = productVariantService.GetVariantsByProduct(productId);

            return Ok(variants);
        }

        [HttpGet("GetVariantBySku")]
        public IActionResult GetVariantBySku([FromQuery] string sku)
        {
            if (string.IsNullOrWhiteSpace(sku))
            {
                return BadRequest("SKU is required.");
            }

            VariantResponseDto? variant = productVariantService.GetVariantBySku(sku);

            if (variant == null)
            {
                return NotFound("Variant was not found.");
            }

            return Ok(variant);
        }

        [HttpPost("AddVariant")]
        public IActionResult AddVariant([FromBody] ProductVariantDTOs dto)
        {
            VariantResponseDto? variant = productVariantService.AddVariant(dto);

            if (variant == null)
            {
                return BadRequest(
                    "Variant could not be added. " +
                    "The Product Id may be invalid or the SKU may already exist.");
            }

            return CreatedAtAction(
                nameof(GetVariantById),
                new { id = variant.variantId },
                variant);
        }

        [HttpPut("UpdateVariant")]
        public IActionResult UpdateVariant([FromQuery] int id,[FromBody] UpdateVariantDto dto)
        {
            if (id <= 0)
            {
                return BadRequest("Variant Id must be greater than 0.");
            }

            VariantResponseDto? variant = productVariantService.UpdateVariant(id, dto);

            if (variant == null)
            {
                return NotFound("Variant was not found.");
            }

            return Ok(variant);
        }

        [HttpPatch("AdjustStock")]
        public IActionResult AdjustStock([FromQuery] int id,[FromBody] AdjustStockDto dto)
        {
            if (id <= 0)
            {
                return BadRequest("Variant Id must be greater than 0.");
            }

            VariantResponseDto? existingVariant = productVariantService.GetVariantById(id);

            if (existingVariant == null)
            {
                return NotFound("Variant was not found.");
            }

            VariantResponseDto? updatedVariant = productVariantService.AdjustStock(id, dto);

            if (updatedVariant == null)
            {
                return BadRequest(
                    "Stock quantity cannot become negative.");
            }

            return Ok(updatedVariant);
        }
    }
}
