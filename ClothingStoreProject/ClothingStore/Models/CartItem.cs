using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace ClothingStore.Models
{
    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int cartItemId { get; set; }

        [Required]
        [ForeignKey("Cart")]
        public int cartId { get; set; }

        [Required]
        [ForeignKey("ProductVariant")]
        public int variantId { get; set; }

        [Required]
        [Range(1, 999)]
        public int quantity { get; set; }

        [Required]
        public DateTime addedAt { get; set; } = DateTime.UtcNow;

        public virtual Cart Cart { get; set; }
        
        // Uncomment once the ProductVariant model is set up in your context
        // public virtual ProductVariant ProductVariant { get; set; }
    }
}
