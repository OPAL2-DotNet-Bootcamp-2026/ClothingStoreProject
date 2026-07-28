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

        [HttpPost("AddItem")]
        public IActionResult AddItem([FromQuery] int userId, [FromBody] AddCartItemDto dto)
        {
            var cart = service.AddItem(userId, dto);
            if (cart == null)
                return BadRequest("Variant does not exist or requested quantity exceeds available stock.");

            return Ok(cart);
        }

        [HttpPut("UpdateItem/{cartItemId}")]
        public IActionResult UpdateItem([FromQuery] int userId, [FromRoute] int cartItemId, [FromBody] UpdateCartItemDto dto)
        {
            var cart = service.UpdateItemQuantity(userId, cartItemId, dto);
            if (cart == null)
                return BadRequest("Cart item not found, not owned by this user, or requested quantity exceeds available stock.");

            return Ok(cart);
        }

        [HttpDelete("RemoveItem/{cartItemId}")]
        public IActionResult RemoveItem([FromQuery] int userId, [FromRoute] int cartItemId)
        {
            var removed = service.RemoveItem(userId, cartItemId);
            if (!removed)
                return NotFound("Cart item not found or not owned by this user.");

            return NoContent();
        }

        [HttpDelete("Clear")]
        public IActionResult ClearCart([FromQuery] int userId)
        {
            var cleared = service.ClearCart(userId);
            if (!cleared)
                return NotFound("Cart not found for this user.");

            return NoContent();
        }
    }
}