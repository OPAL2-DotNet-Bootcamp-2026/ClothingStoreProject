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
    }
}
