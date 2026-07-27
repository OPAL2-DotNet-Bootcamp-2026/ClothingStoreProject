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

    }
}
