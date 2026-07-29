using ClothingStore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;
using static ClothingStore.DTOs.UserDTOs;

namespace ClothingStore.Controllers
{
    [EnableRateLimiting("public")]
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
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllUsers()

        {

            List<UserResponseDto> users = userService.GetAllUsers();

            return Ok(users);

        }

        // GET: user/GetUserById?id=1

        [HttpGet("GetUserById")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetUserById([FromQuery] int id)

        {

            UserResponseDto? user = userService.GetUserById(id);

            if (user == null)
            {
                return NotFound("user not found ");
            }

            return Ok(user);

        }




        // GET: user/GetUsersByRole?role=Customer

        [HttpGet("GetUsersByRole")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetUsersByRole([FromQuery] Enums.Role role)

        {
            List<UserResponseDto> users = userService.GetUsersByRole(role);

            return Ok(users);

        }




        // POST: user/RegisterUser

        [HttpPost("RegisterUser")]
        [AllowAnonymous]
        public IActionResult RegisterUser([FromBody] RegisterUserDto dto)

        {

            UserResponseDto? user = userService.RegisterUser(dto);

            if (user == null)
            {
                return BadRequest("Email or Username already exists.");

            }
            return Created();

        }





        // POST: user/LoginUser

        [HttpPost("LoginUser")]
        [AllowAnonymous]
        public IActionResult LoginUser([FromBody] LoginDto dto)

        {
            LoginResponseDto? user = userService.LoginUser(dto);

            if (user == null)
            {
                return Unauthorized("Invalid email or  password ");
            }

            return Ok(user);

        }



        // PUT: user/UpdateUserProfile?id=1
        [HttpPut("UpdateUserProfile")]
        [Authorize(Roles = "Customer")]
        public IActionResult UpdateUserProfile([FromQuery] int id, [FromBody] UpdateUserDto dto)
        {
            UserResponseDto? user = userService.UpdateUserProfile(id, dto);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }




        // PUT: user/ChangeUserPassword?id=1
        [HttpPut("ChangeUserPassword")]
        [Authorize(Roles = "Customer")]
        public IActionResult ChangeUserPassword([FromQuery] int id, [FromBody] ChangePasswordDto dto)
        {
            int? userId = GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized(
                    "The token does not contain a valid userId.");
            }
            bool changed =
                userService.ChangeUserPassword(
                    userId.Value,
                    dto);

            if (!changed)
            {
                return BadRequest("Password change failed.");
            }

            return Ok("Password changed successfully.");
        }






        // PATCH: user/SetUserActiveStatus?id=1
        [HttpPatch("SetUserActiveStatus")]
        [Authorize(Roles = "Admin")]
        public IActionResult SetUserActiveStatus([FromQuery] int id, [FromBody] SetActiveStatusDto dto)
        {
            bool updated = userService.SetUserActiveStatus(id, dto);

            if (!updated)
            {
                return NotFound("User not found.");
            }

            return Ok("User status updated successfully.");
        }

        [HttpPatch("SetUserRoleToAdmin")]
        [Authorize(Roles = "Admin")]
        public IActionResult SetUserRole([FromQuery] int id, [FromBody] SetRoleToAdmin dto)
        {
            bool updated = userService.SetUserRole(id, dto);

            if (!updated)
            {
                return NotFound("User not found.");
            }

            return Ok("User status updated successfully.");
        }

        private int? GetCurrentUserId()
        {
            string? userIdValue =
                User.FindFirst("userId")?.Value;

            if (!int.TryParse(
                    userIdValue,
                    out int userId))
            {
                return null;
            }

            return userId;
        }

    }
}































