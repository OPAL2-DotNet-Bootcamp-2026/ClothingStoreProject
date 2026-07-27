using ClothingStore.DTOs;
using ClothingStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.Controllers
{
    [Route("order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private OrderService service;

        public OrderController(OrderService _service)
        {
            service = _service;
        }

        // TODO: replace [FromQuery] int userId with User.FindFirst("userId") once
        // JWT auth middleware is wired into Program.cs.

        [HttpGet]
        // [Authorize admi rol] 
        public IActionResult GetAll()
        {
            var orders = service.GetAll();
            return Ok(orders);
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetByUser([FromRoute] int userId)
        {
            var orders = service.GetByUserId(userId);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var order = service.GetById(id);
            if (order == null)
                return NotFound($"Order with id {id} not found.");

            return Ok(order);
        }

        [HttpPost("Checkout")]
        public IActionResult Checkout([FromQuery] int userId, [FromBody] CreateOrderDto dto)
        {
            var order = service.Checkout(userId, dto);
            if (order == null)
                return BadRequest("Cart is empty, or one or more items no longer have enough stock.");

            return CreatedAtAction(nameof(GetById), new { id = order.OrderId }, order);
        }

        [HttpPatch("UpdateStatus/{id}")]
        // [Authorize admin role] 
        public IActionResult UpdateStatus([FromRoute] int id, [FromBody] UpdateOrderStatusDto dto)
        {
            var updated = service.UpdateStatus(id, dto);
            if (!updated)
                return NotFound($"Order with id {id} not found.");

            return NoContent();
        }
    }
}