using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static ClothingStore.Enums;

namespace ClothingStore.Models
{
    [Index(nameof(productName), IsUnique = true)]
    public class Product
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int productId { get; set; } // System Generated 

        [Required]
        [MaxLength(150)]
        public string productName { get; set; } // User Input

        [MaxLength(1000)]
        public string? description { get; set; } // User Input

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Base price must be greater than 0.")]
        public decimal basePrice { get; set; } // User Input

        // Foreign Key & Navigation for Brand
        [Required]
        [ForeignKey(nameof(Brand))]
        public int BrandId { get; set; } // From Brand List
        public virtual Brand Brand { get; set; } // Navigation Property

        // Foreign Key & Navigation for Category
        [Required]
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; } // From Category List
        public virtual Category Category { get; set; } // Navigation Property

        [Required]
        public Gender gender { get; set; } // User Input (Enum)

        [MaxLength(100)]
        public string? material { get; set; } // User Input

        [Required]
        public ClothingStyle clothingStyle { get; set; } // User Input (Enum)

        public Season? season { get; set; } // User Input (Enum)

        [MaxLength(300)]
        public string? careInstructions { get; set; } // User Input

        [Required]
        public DateTime createdAt { get; set; } // System Calculated

        public bool isAvailable { get; set; } = true; // Default Value

        // Navigation property for Reviews
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>(); // Navigation Property

        // Navigation Property For ProductVariants
        public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();// Navigation Property
    }
}
