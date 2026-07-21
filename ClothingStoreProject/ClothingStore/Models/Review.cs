using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothingStore.Models
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int reviewId { get; set; }//system generated 


        [Required(ErrorMessage = "Rating is required.")]
        [Range(1, 5,ErrorMessage = "Rating must be between 1 and 5.")]
        public int rating { get; set; }//user input — 1 to 5 stars


        [MaxLength(1000,ErrorMessage = "Comment cannot exceed 1000 characters.")]
        public string? comment { get; set; }//user input 


        [Required(ErrorMessage = "Review date is required.")]
        public DateTime reviewDate { get; set; } // system generated — set to today's date


        [ForeignKey("user")]//foreign  key to user 
        [Required(ErrorMessage = "User ID is required.")]
        public int userId { get; set; }//from the list — from logged-in user
        public User user { get; set; }// navigation property ,(user & review)



        [ForeignKey("product")]//foreign  key to product
        [Required(ErrorMessage = "Product ID is required.")]
        public int productId { get; set; }//from the list  — chosen from purchased products
        public Product product { get; set; }//navigation property ,(product & review)
    }
}
