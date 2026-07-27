using ClothingStore.DTOs;
using ClothingStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStore.Controllers{

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
        // JWT auth middleware is wired into Program.cs
        
        [HttpGet]
        // [Authorize(Roles = "Admin")] — enable once auth middleware is registered
        public IActionResult GetAll()
        {
            var orders = service.GetAll();
            return Ok(orders);
        }
        
        
        
    }


}
