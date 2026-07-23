using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothingStore.Models
{


    [Index(nameof(userName), nameof(email), IsUnique = true)]

    public class User
    {
   

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userId { get; set; } //system generated 


        [Required(ErrorMessage = "Username can't be empty!!")]
        [MaxLength(50,ErrorMessage = "Username can't be more than 50 characters!!")]
        public string userName { get; set; }   //user input 


        [Required(ErrorMessage = "Email can't be empty!!")]
        [MaxLength(150,ErrorMessage = "Email can't be more than 150 characters!!")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string email { get; set; } //user input 


        [Required(ErrorMessage = "Password can't be empty!!")]
        [MaxLength(256,ErrorMessage = "Password can't be more than 256 characters!!")]
        public string passwordHash { get; set; }  //user input 


        [Required(ErrorMessage = "Full name can't be empty!!")]
        [MaxLength(100,ErrorMessage = "Full name can't be more than 100 characters!!")]
        public string fullName { get; set; }  //user input 


        [RegularExpression(@"^[79]\d{7}$", ErrorMessage = "Please enter a valid Omani phone number.")]
        public string? phoneNumber { get; set; } //user input


        [MaxLength(300,ErrorMessage = "Address can't be more than 300 characters!!")]
        public string? address { get; set; }  //user input 


        [Required(ErrorMessage = "Registration date is required!!")]
        public DateTime registrationDate { get; set; }  //system generated — set to today's date
        public bool isActive { get; set; } = true; // default value


        [Required (ErrorMessage = "Role is required.")]
        [MaxLength(20,ErrorMessage = "Role cannot exceed 20 characters.")]
        public string role { get; set; } = "customer"; //from list by defualt = customer


        public List<Review> reviews { get; set; }= new List<Review>();//reverse navigation — one user has many Reviews









    }
}
