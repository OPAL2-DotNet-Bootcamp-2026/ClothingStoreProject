using ClothingStore.Services;
using Microsoft.AspNetCore.Mvc;
using static ClothingStore.DTOs.UserDTOs;

namespace ClothingStore.Controllers
{
    public class UserController
    {

        [ApiController]
        [Route("user")]
        public class UserController : ControllerBase
        {
            private UserService userService;

            public UserController(UserService userService)
            {
                this.userService = userService;
            }



            // GET: user/GetAllUsers

            [HttpGet("GetAllUsers")]

            public IActionResult GetAllUsers()

            {

                List<UserResponseDto> users = userService.GetAllUsers();

                return Ok(users);

            }

            // GET: user/GetUserById?id=1

            [HttpGet("GetUserById")]

            public IActionResult GetUserById([FromQuery] int id)

            {

                UserResponseDto user = userService.GetUserById(id);

                return Ok(user);

            }

            // GET: user/GetUsersByRole?role=Customer

            [HttpGet("GetUsersByRole")]

            public IActionResult GetUsersByRole([FromQuery] string role)

            {

                List<UserResponseDto> users = userService.GetUsersByRole(role);

                return Ok(users);

            }


































        }


    }


        }
    
