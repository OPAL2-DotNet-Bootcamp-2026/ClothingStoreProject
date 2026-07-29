using ClothingStore.Repos;
using ClothingStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using static ClothingStore.DTOs.OrderDTOs;

namespace ClothingStore.Controllers
{
    [Authorize]
    [EnableRateLimiting("private")]
    [Route("order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _service;
        private readonly IEmailService _emailService;
        private readonly UserRepo _userRepo;

        public OrderController(
            OrderService service,
            IEmailService emailService,
            UserRepo userRepo)
        {
            _service = service;
            _emailService = emailService;
            _userRepo = userRepo;
        }

        // Admin: GET /order
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var orders = _service.GetAll();

            return Ok(orders);
        }

        // Admin: GET /order/user/1
        [Authorize(Roles = "Admin")]
        [HttpGet("user/{userId}")]
        public IActionResult GetByUser(
            [FromRoute] int userId)
        {
            if (userId <= 0)
            {
                return BadRequest(
                    "User Id must be greater than zero.");
            }

            var orders = _service.GetByUserId(userId);

            return Ok(orders);
        }

        // Admin: GET /order/1
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public IActionResult GetById(
            [FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest(
                    "Order Id must be greater than zero.");
            }

            var order = _service.GetById(id);

            if (order == null)
            {
                return NotFound(
                    $"Order with id {id} was not found.");
            }

            return Ok(order);
        }

        // Customer: GET /order/mine
        [Authorize(Roles = "Customer")]
        [HttpGet("mine")]
        public IActionResult GetMyOrders()
        {
            int? userId = GetCurrentUserId();

            if (userId == null)
            {
                return Unauthorized(
                    "The token does not contain a valid userId.");
            }

            var orders = _service.GetByUserId(userId.Value);

            return Ok(orders);
        }

        // Customer: GET /order/mine/1
        [Authorize(Roles = "Customer")]
        [HttpGet("mine/{id}")]
        public IActionResult GetMyOrderById(
            [FromRoute] int id)
        {
            int? userId = GetCurrentUserId();

            if (userId == null)
            {
                return Unauthorized(
                    "The token does not contain a valid userId.");
            }

            if (id <= 0)
            {
                return BadRequest(
                    "Order Id must be greater than zero.");
            }

            var order = _service.GetById(id);

            // Return NotFound when the order does not exist
            // or does not belong to the logged-in customer.
            if (order == null || order.UserId != userId.Value)
            {
                return NotFound(
                    $"Order with id {id} was not found.");
            }

            return Ok(order);
        }

        // Customer: POST /order/Checkout
        [Authorize(Roles = "Customer")]
        [HttpPost("Checkout")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        public async Task<IActionResult> Checkout(
            [FromBody] CreateOrderDto dto)
        {
            int? userId = GetCurrentUserId();

            if (userId == null)
            {
                return Unauthorized(
                    "The token does not contain a valid userId.");
            }

            var order = _service.Checkout(
                userId.Value,
                dto);

            if (order == null)
            {
                return BadRequest(
                    "The cart is empty, or one or more items " +
                    "no longer have enough stock.");
            }

            var user = _userRepo.GetUserById(userId.Value);

            if (user != null)
            {
                await _emailService.SendAsync(
                    user.email,
                    $"Order #{order.OrderId} Confirmed",
                    $"""
                    <h1>Thank you, {user.fullName}!</h1>

                    <p>
                        Your order
                        <strong>#{order.OrderId}</strong>
                        has been placed successfully.
                    </p>

                    <p>
                        <strong>Total:</strong>
                        OMR {order.TotalAmount:N3}
                    </p>

                    <p>
                        We will notify you when the order status changes.
                    </p>

                    <p>
                        Kind regards,<br>
                        <strong>ClothingStore Team</strong>
                    </p>
                    """);
            }

            return CreatedAtAction(
                nameof(GetMyOrderById),
                new { id = order.OrderId },
                order);
        }

        // Admin: PATCH /order/UpdateStatus/1
        [Authorize(Roles = "Admin")]
        [HttpPatch("UpdateStatus/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        public async Task<IActionResult> UpdateStatus(
            [FromRoute] int id,
            [FromBody] UpdateOrderStatusDto dto)
        {
            if (id <= 0)
            {
                return BadRequest(
                    "Order Id must be greater than zero.");
            }

            var order = _service.UpdateStatus(id, dto);

            if (order == null)
            {
                return NotFound(
                    $"Order with id {id} was not found.");
            }

            var user = _userRepo.GetUserById(order.UserId);

            if (user != null)
            {
                await _emailService.SendAsync(
                    user.email,
                    $"Order #{id} Status Updated",
                    $"""
                    <h1>Hello {user.fullName},</h1>

                    <p>
                        The status of your order
                        <strong>#{id}</strong>
                        has been updated.
                    </p>

                    <p>
                        <strong>New status:</strong>
                        {dto.Status}
                    </p>

                    <p>
                        Thank you for shopping with ClothingStore.
                    </p>

                    <p>
                        Kind regards,<br>
                        <strong>ClothingStore Team</strong>
                    </p>
                    """);
            }

            return NoContent();
        }

        private int? GetCurrentUserId()
        {
            string? userIdValue =
                User.FindFirst("userId")?.Value;

            if (!int.TryParse(userIdValue, out int userId))
            {
                return null;
            }

            return userId;
        }
    }
}