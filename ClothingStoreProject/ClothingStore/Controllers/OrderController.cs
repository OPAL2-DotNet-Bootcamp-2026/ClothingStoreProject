using ClothingStore.Repos;
using ClothingStore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using static ClothingStore.DTOs.OrderDTOs;

namespace ClothingStore.Controllers
{
    [EnableRateLimiting("public")]
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

        // GET: order
        [HttpGet]
        public IActionResult GetAll()
        {
            var orders = _service.GetAll();

            return Ok(orders);
        }

        // GET: order/user/1
        [HttpGet("user/{userId}")]
        public IActionResult GetByUser([FromRoute] int userId)
        {
            var orders = _service.GetByUserId(userId);

            return Ok(orders);
        }

        // GET: order/1
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var order = _service.GetById(id);

            if (order == null)
            {
                return NotFound($"Order with id {id} was not found.");
            }

            return Ok(order);
        }

        // POST: order/Checkout?userId=1
        [HttpPost("Checkout")]
        [EnableRateLimiting("private")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        public async Task<IActionResult> Checkout(
            [FromQuery] int userId,
            [FromBody] CreateOrderDto dto)
        {
            var order = _service.Checkout(userId, dto);

            if (order == null)
            {
                return BadRequest(
                    "The cart is empty, or one or more items " +
                    "no longer have enough stock.");
            }

            var user = _userRepo.GetUserById(userId);

            if (user != null)
            {
                await _emailService.SendAsync(
                    user.email,
                    "Order Confirmed",
                    $"""
                    <h1>Thank you, {user.fullName}!</h1>

                    <p>
                        Your order #{order.OrderId} has been placed successfully.
                    </p>

                    <p>
                        Total: {order.TotalAmount:C}
                    </p>
                    """);
            }

            return CreatedAtAction(
                nameof(GetById),
                new { id = order.OrderId },
                order);
        }

        // PATCH: order/UpdateStatus/1
        [HttpPatch("UpdateStatus/{id}")]
        [EnableRateLimiting("private")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        public async Task<IActionResult> UpdateStatus(
            [FromRoute] int id,
            [FromBody] UpdateOrderStatusDto dto)
        {
            var order = _service.UpdateStatus(id, dto);

            if (order == null)
            {
                return NotFound($"Order with id {id} was not found.");
            }

            var user = _userRepo.GetUserById(order.UserId);

            if (user != null)
            {
                await _emailService.SendAsync(
                    user.email,
                    "Order Status Updated",
                    $"""
                    <h1>Hi {user.fullName},</h1>

                    <p>
                        Your order #{id} status has been updated to
                        <strong>{dto.Status}</strong>.
                    </p>
                    """);
            }

            return NoContent();
        }
    }
}