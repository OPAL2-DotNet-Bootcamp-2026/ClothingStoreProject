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
        
    }


}
