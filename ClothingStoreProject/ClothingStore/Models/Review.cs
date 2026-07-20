using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothingStore.Models
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int reviewId { get; set; }

        [Required]
        [Range(1, 5)]
        public int rating { get; set; }

        [MaxLength(1000)]
        public string? comment { get; set; }
        [Required]
        public DateTime reviewDate { get; set; }


        [ForeignKey("user")]//foreign  key to user 
        public int userId { get; set; }
        public User user { get; set; }


        [ForeignKey("product")]//foreign  key to product
        public int productId { get; set; }
        public Product product { get; set; }
    }
}
