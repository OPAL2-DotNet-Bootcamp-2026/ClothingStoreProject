using ClothingStore.Services;
using Microsoft.AspNetCore.Mvc;
using static ClothingStore.DTOs.UserDTOs;

namespace ClothingStore.Controllers


{

    [ApiController]

    public class UserController : ControllerBase
    {
        
            private UserService userService;

            public UserController(UserService userService)
            {
              this.userService = userService;
            }



        // POST user/RegisterUser
        [HttpPost("RegisterUser")]
        public IActionResult RegisterUser([FromBody] RegisterUserDto dto)
        {
            userService.RegisterUser(dto);

            return Ok(new { message = "User registered successfully." });
        }

        // POST user/LoginUser
        [HttpPost("LoginUser")]
        public IActionResult LoginUser([FromBody] LoginDto dto)
        {
            var user = userService.LoginUser(dto);

            if (user == null)
                return Unauthorized(new { message = "Invalid username or password." });

            return Ok(user);
        }

        // GET user/GetAllUsers
        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            return Ok(userService.GetAllUsers());
        }

        // GET user/GetUserById?id=1
        [HttpGet("GetUserById")]
        public IActionResult GetUserById([FromQuery] int id)
        {
            var user = userService.GetUserById(id);

            if (user == null)
                return NotFound(new { message = "User not found." });

            return Ok(user);
        }

        // PUT user/UpdateUserProfile?id=1
        [HttpPut("UpdateUserProfile")]
        public IActionResult UpdateUserProfile([FromQuery] int id, [FromBody] UpdateUserDto dto)
        {
            userService.UpdateUserProfile(id, dto);

            return Ok(new { message = "Profile updated successfully." });
        }

        // PUT user/ChangeUserPassword?id=1
        [HttpPut("ChangeUserPassword")]
        public IActionResult ChangeUserPassword([FromQuery] int id, [FromBody] ChangePasswordDto dto)
        {
            userService.ChangeUserPassword(id, dto);

            return Ok(new { message = "Password changed successfully." });
        }

        // PATCH user/SetUserActiveStatus?id=1
        [HttpPatch("SetUserActiveStatus")]
        public IActionResult SetUserActiveStatus([FromQuery] int id, [FromBody] SetActiveStatusDto dto)
        {
            userService.SetUserActiveStatus(id, dto);

            return Ok(new { message = "User status updated successfully." });











        }
    }
