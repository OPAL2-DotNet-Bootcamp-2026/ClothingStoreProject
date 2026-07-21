using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace ClothingStore.Models
{
    public class ProductVariant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int variantId { get; set; } // System Generated

        // Foreign Key & Navigation for Product
        [Required(ErrorMessage = "Product ID is required.")]
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; } // Foreign Key to Product

        public virtual Product Product { get; set; } // Navigation Property

        [Required(ErrorMessage = "SKU is required.")]
        [MaxLength(50, ErrorMessage = "SKU cannot exceed 50 characters.")]
        public string sku { get; set; } // User Input 

        [Required(ErrorMessage = "Size is required.")]
        [MaxLength(10, ErrorMessage = "Size cannot exceed 10 characters.")]
        public Size size { get; set; } // User Input (Enum)

        [Required(ErrorMessage = "Color is required.")]
        [MaxLength(30, ErrorMessage = "Color cannot exceed 30 characters.")]
        public string color { get; set; } // User Input

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal price { get; set; } // User Input

        [Required(ErrorMessage = "Stock quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity must be greater than or equal to 0.")]
        public int stockQuantity { get; set; } = 0; // Default 0

        [MaxLength(300, ErrorMessage = "Image URL cannot exceed 300 characters.")]
        [Url(ErrorMessage = "Please enter a valid URL for the image.")]
        public string? imageUrl { get; set; } // User Input

        // Navigation Properties for CartItems and OrderItems
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>(); 
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>(); 
    }
}
