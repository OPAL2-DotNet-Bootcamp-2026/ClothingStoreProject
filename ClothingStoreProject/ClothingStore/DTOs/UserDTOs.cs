using System.ComponentModel.DataAnnotations;

namespace ClothingStore.DTOs
{
    public class UserDTOs
    {


        //UserResponseDto (no passwordHash)

        public class UserResponseDto
        {
            public int UserId { get; set; }

            public string Username { get; set; }

            public string Email { get; set; }

            public string FullName { get; set; }

            public string? PhoneNumber { get; set; }

            public string? Address { get; set; }

            public DateTime RegistrationDate { get; set; }

            public bool IsActive { get; set; }

            public string Role { get; set; }
        }



        //RegisterUserDto

        public class RegisterUserDto
        {
            [Required]
            [MaxLength(50)]
            public string userName { get; set; }

            [Required]
            [EmailAddress]
            [MaxLength(150)]
            public string email { get; set; }

            [Required]
            [MaxLength(100)]
            public string passwordHash { get; set; }

            [Required]
            [MaxLength(100)]
            public string fullName { get; set; }

            [MaxLength(20)]
            public string? phoneNumber { get; set; }

            [MaxLength(300)]
            public string? address { get; set; }
        }



        // LoginDto

        public class LoginDto
        {
            [Required]
            public string email { get; set; }

            [Required]
            public string passwordHash { get; set; }
        }



        //UpdateUserDto

        public class UpdateUserDto
        {
            [Required]
            [MaxLength(50)]
            public string userName { get; set; }

            [Required]
            [EmailAddress]
            [MaxLength(150)]
            public string email { get; set; }

            [Required]
            [MaxLength(100)]
            public string fullName { get; set; }

            [MaxLength(20)]
            public string? phoneNumber { get; set; }

            [MaxLength(300)]
            public string? address { get; set; }
        }



        //ChangePasswordDto

        public class ChangePasswordDto
        {
            [Required]
            public string CurrentPassword { get; set; }

            [Required]
            [MaxLength(100)]
            public string NewPassword { get; set; }

            [Required]
            [Compare("NewPassword")]
            public string ConfirmPassword { get; set; }
        }









    }
}
