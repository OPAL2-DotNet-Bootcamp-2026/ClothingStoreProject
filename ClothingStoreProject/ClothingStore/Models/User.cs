using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothingStore.Models
{


    [Index(nameof(userName), nameof(email), IsUnique = true)]

    public class User
    {



        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userId { get; set; }


        [Required]
        [MaxLength(50)]
        public string userName { get; set; }

        [Required]
        [MaxLength(150)]
        public string email { get; set; } 

        [Required]
        [MaxLength(256)]
        public string passwordHash { get; set; }

        [Required]
        [MaxLength(100)]
        public string fullName { get; set; }

        [MaxLength(20)]
        public string? phoneNumber { get; set; }

        [MaxLength(300)]
        public string? address { get; set; }

        [Required]
        public DateTime registrationDate { get; set; }

        public bool isActive { get; set; } = true;



        public List<Review> reviews { get; set; }









    }
}
