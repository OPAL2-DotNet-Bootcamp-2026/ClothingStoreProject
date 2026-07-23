using System.ComponentModel.DataAnnotations;

namespace ClothingStore.DTOs
{
    public class UserDTOs
    {


        //UserResponseDto (no passwordHash)

        public class UserResponseDto
        {
            public int userId { get; set; }

            public string userName { get; set; }

            public string email { get; set; }

            public string fullName { get; set; }

            public string? phoneNumber { get; set; }

            public string? address { get; set; }

            public DateTime registrationDate { get; set; }

            public bool isActive { get; set; }

            public string role { get; set; }
        }



        //RegisterUserDto

        public class RegisterUserDto
        {

            [Required(ErrorMessage = "Username can't be empty!!")]
            [MaxLength(50, ErrorMessage = "Username can't be more than 50 characters!!")]
            public string userName { get; set; }


            [Required(ErrorMessage = "Email is required.")]
            [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
            [MaxLength(150,ErrorMessage = "Email cannot exceed 150 characters.")]
            public string email { get; set; }


            [Required(ErrorMessage = "Password is required.")]
            [RegularExpression(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$",
            ErrorMessage = "Password must be at least 8 characters long and contain uppercase, lowercase, number and special character.")]
            public string password{ get; set; }



            [Required(ErrorMessage = "Full name can't be empty!!")]
            [MaxLength(100,ErrorMessage = "Full name can't be more than 100 characters!!")]
            public string fullName { get; set; }


            [RegularExpression(@"^[79]\d{7}$",ErrorMessage = "Please enter a valid Omani phone number.")]
            public string? phoneNumber { get; set; }


            [MaxLength(300,ErrorMessage = "Address can't be more than 300 characters!!")]
            public string? address { get; set; }
        }



        // LoginDto

        public class LoginDto
        {
            [Required(ErrorMessage = "Email is required.")]
            [EmailAddress(ErrorMessage = "Invalid email format.")]
            public string email { get; set; }

            [Required(ErrorMessage = "Password is required.")]
            public string password{ get; set; }
        }



        //UpdateUserDto

        public class UpdateUserDto
        {
           
            [Required(ErrorMessage = "Username is required.")]
            [MaxLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
            public string userName { get; set; }


            [Required(ErrorMessage = "Email is required.")]
            [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
            [MaxLength(150, ErrorMessage = "Email cannot exceed 150 characters.")]
            public string email { get; set; }



            [Required(ErrorMessage = "Full name can't be empty!!")]
            [MaxLength(100, ErrorMessage = "Full name can't be more than 100 characters!!")]
            public string fullName { get; set; }


            [RegularExpression(@"^[79]\d{7}$", ErrorMessage = "Please enter a valid Omani phone number.")]
            public string? phoneNumber { get; set; }



            [MaxLength(300, ErrorMessage = "Address can't be more than 300 characters!!")]
            public string? address { get; set; }
        }





        //ChangePasswordDto

        public class ChangePasswordDto
        {
            [Required(ErrorMessage = "Current password is requied")]
            public string currentPassword { get; set; }


            [Required(ErrorMessage = "New password is required.")]
            [RegularExpression(
             @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$",
            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, one special character, and be at least 8 characters long.")]
            public string newPassword { get; set; }



            [Required(ErrorMessage = "Please confirm your new password.")]
            [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
            public string confirmPassword { get; set; }
        }









    }
}
