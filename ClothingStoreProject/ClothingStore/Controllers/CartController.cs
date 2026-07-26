using ClothingStore.DTOs;
using ClothingStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.Controllers
{
    [Route("cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private CartService service;

        public CartController(CartService _service)
        {
            service = _service;
        }
        
        // TODO: replace [FromQuery] int userId with User.FindFirst("userId") once
        // JWT auth middleware is wired into Program.cs. Left as a query param for now
        // so the endpoint is testable in Swagger before auth exists.
        
        [HttpGet("{userId}")]
        public IActionResult GetCart([FromRoute] int userId)
        {
            var cart = service.GetByUserId(userId);
            return Ok(cart);
        }
        
    }
}
