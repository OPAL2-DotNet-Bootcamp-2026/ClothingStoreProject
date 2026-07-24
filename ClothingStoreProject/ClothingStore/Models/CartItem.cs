using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace ClothingStore.Models
{
    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int cartItemId { get; set; } // System Generated

        [Required(ErrorMessage = "Cart ID is required.")]
        [ForeignKey(nameof(Cart))]
        public int cartId { get; set; } // From Cart List
        public virtual Cart Cart { get; set; } // Navigation Property

        [Required(ErrorMessage = "Variant ID is required.")]
        [ForeignKey(nameof(ProductVariant))]
        public int variantId { get; set; } // From ProductVariant List
        public virtual ProductVariant ProductVariant { get; set; } // Navigation Property

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, 999, ErrorMessage = "Quantity must be between 1 and 999.")]
        public int quantity { get; set; } // User Input

        [Required(ErrorMessage = "Added date is required.")]
        public DateTime addedAt { get; set; } = DateTime.UtcNow; // System Calculated
    }
}
