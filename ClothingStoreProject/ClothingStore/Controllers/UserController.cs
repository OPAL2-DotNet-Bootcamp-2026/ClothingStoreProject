using ClothingStore.Services;
using Microsoft.AspNetCore.Mvc;
using static ClothingStore.DTOs.UserDTOs;

namespace ClothingStore.Controllers
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

                UserResponseDto ? user = userService.GetUserById(id);

                if (user == null)
                {
                    return NotFound("user not found ");
                }

                return Ok(user);

            }




            // GET: user/GetUsersByRole?role=Customer

            [HttpGet("GetUsersByRole")]

            public IActionResult GetUsersByRole([FromQuery] Enums.Role role)

            {
                List<UserResponseDto> users = userService.GetUsersByRole(role);

                return Ok(users);

            }




            // POST: user/RegisterUser

            [HttpPost("RegisterUser")]

            public IActionResult RegisterUser([FromBody] RegisterUserDto dto)

            {

                UserResponseDto ? user = userService.RegisterUser(dto);

                if (user == null)
                {
                    return BadRequest("Email or Username already exists.");

                }
                return Ok(user);

            }





            // POST: user/LoginUser

            [HttpPost("LoginUser")]

            public IActionResult LoginUser([FromBody] LoginDto dto)

            {
                UserResponseDto? user = userService.LoginUser(dto);
                 
                if(user == null)
                { 
                    return  Unauthorized("Invalid email or  password ");
                }

                return Ok(user);

            }



            // PUT: user/UpdateUserProfile?id=1
            [HttpPut("UpdateUserProfile")]

            public IActionResult UpdateUserProfile( [FromQuery] int id, [FromBody] UpdateUserDto dto)
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
              public IActionResult ChangeUserPassword([FromQuery] int id, [FromBody] ChangePasswordDto dto)
                 {
                bool changed = userService.ChangeUserPassword(id, dto);

                if (!changed)
                {
                    return BadRequest("Password change failed.");
                }

                return Ok("Password changed successfully.");
                 }




         
            
            // PATCH: user/SetUserActiveStatus?id=1
            [HttpPatch("SetUserActiveStatus")]

            public IActionResult SetUserActiveStatus( [FromQuery] int id, [FromBody] SetActiveStatusDto dto)
            {
                bool updated = userService.SetUserActiveStatus(id, dto);

                if (!updated)
                {
                    return NotFound("User not found.");
                }

                return Ok("User status updated successfully.");
            }
        }
    }






























    
