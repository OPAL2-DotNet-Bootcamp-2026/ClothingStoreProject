using ClothingStore.DTOs;
using ClothingStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;

namespace ClothingStore.Controllers
{
    [Authorize(Roles = "Customer")]
    [EnableRateLimiting("private")]
    [Route("cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartService _service;

        public CartController(CartService service)
        {
            _service = service;
        }

        // GET: cart
        [HttpGet]
        public IActionResult GetCart()
        {
            int? userId = GetCurrentUserId();

            if (userId == null)
            {
                return Unauthorized(
                    "The token does not contain a valid userId.");
            }

            var cart = _service.GetByUserId(userId.Value);

            if (cart == null)
            {
                return NotFound("No cart was found for this user.");
            }

            return Ok(cart);
        }

        // POST: cart/AddItem
        [HttpPost("AddItem")]
        public IActionResult AddItem(
            [FromBody] AddCartItemDto dto)
        {
            int? userId = GetCurrentUserId();

            if (userId == null)
            {
                return Unauthorized(
                    "The token does not contain a valid userId.");
            }

            var cart = _service.AddItem(userId.Value, dto);

            if (cart == null)
            {
                return BadRequest(
                    "The variant does not exist, or the requested " +
                    "quantity exceeds the available stock.");
            }

            return Ok(cart);
        }

        // PUT: cart/UpdateItem/5
        [HttpPut("UpdateItem/{cartItemId}")]
        public IActionResult UpdateItem(
            [FromRoute] int cartItemId,
            [FromBody] UpdateCartItemDto dto)
        {
            int? userId = GetCurrentUserId();

            if (userId == null)
            {
                return Unauthorized(
                    "The token does not contain a valid userId.");
            }

            var cart = _service.UpdateItemQuantity(
                userId.Value,
                cartItemId,
                dto);

            if (cart == null)
            {
                return BadRequest(
                    "The cart item was not found, does not belong " +
                    "to this user, or the requested quantity " +
                    "exceeds the available stock.");
            }

            return Ok(cart);
        }

        // DELETE: cart/RemoveItem/5
        [HttpDelete("RemoveItem/{cartItemId}")]
        public IActionResult RemoveItem(
            [FromRoute] int cartItemId)
        {
            int? userId = GetCurrentUserId();

            if (userId == null)
            {
                return Unauthorized(
                    "The token does not contain a valid userId.");
            }

            bool removed = _service.RemoveItem(
                userId.Value,
                cartItemId);

            if (!removed)
            {
                return NotFound(
                    "The cart item was not found or does not " +
                    "belong to this user.");
            }

            return NoContent();
        }

        // DELETE: cart/Clear
        [HttpDelete("Clear")]
        public IActionResult ClearCart()
        {
            int? userId = GetCurrentUserId();

            if (userId == null)
            {
                return Unauthorized(
                    "The token does not contain a valid userId.");
            }

            bool cleared = _service.ClearCart(userId.Value);

            if (!cleared)
            {
                return NotFound(
                    "No cart was found for this user.");
            }

            return NoContent();
        }

        private int? GetCurrentUserId()
        {
            string? userIdClaim =
                User.FindFirstValue("userId");

            if (!int.TryParse(userIdClaim, out int userId))
            {
                return null;
            }

            return userId;
        }
    }
}