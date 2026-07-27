using ClothingStore.DTOs;
using ClothingStore.Repos;
using ClothingStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.Controllers
{
    [Route("order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private OrderService service;
        private readonly IEmailService _emailService;
        private readonly UserRepo _userRepo;

        public OrderController(OrderService _service, IEmailService emailService, UserRepo userRepo)
        {
            service = _service;
            _emailService = emailService;
            _userRepo = userRepo;
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
        public async Task<IActionResult> Checkout([FromQuery] int userId, [FromBody] CreateOrderDto dto)
        {
            var order = service.Checkout(userId, dto);
            if (order == null)
                return BadRequest("Cart is empty, or one or more items no longer have enough stock.");

            var user = _userRepo.GetUserById(userId);
            if (user != null)
                await _emailService.SendAsync(
                    user.email,
                    "Order Confirmed",
                    $"<h1>Thank you, {user.fullName}!</h1><p>Your order #{order.OrderId} has been placed successfully. Total: {order.TotalAmount:C}</p>"
                );

            return CreatedAtAction(nameof(GetById), new { id = order.OrderId }, order);
        }

        [HttpPatch("UpdateStatus/{id}")]
        // Authorize admin role
        public async Task<IActionResult> UpdateStatus([FromRoute] int id, [FromBody] UpdateOrderStatusDto dto)
        {
            var order = service.GetById(id);
            if (order == null)
                return NotFound($"Order with id {id} not found.");

            service.UpdateStatus(id, dto);

            var user = _userRepo.GetUserById(order.UserId);
            if (user != null)
                await _emailService.SendAsync(
                    user.email,
                    "Order Status Updated",
                    $"<h1>Hi {user.fullName},</h1><p>Your order #{id} status has been updated to <strong>{dto.Status}</strong>.</p>"
                );

            return NoContent();
        }
    }
}