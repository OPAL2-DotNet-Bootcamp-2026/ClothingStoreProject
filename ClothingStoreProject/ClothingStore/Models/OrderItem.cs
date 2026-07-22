using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothingStore.Models
{
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int orderItemId { get; set; } // System Generated

        // Foreign Key & Navigation for Order
        [Required(ErrorMessage = "Order ID is required.")]
        [ForeignKey(nameof(Order))]
        public int orderId { get; set; } // Foreign Key - Calculated from Order Class

        public virtual Order Order { get; set; } // Navigation Property

        // Foreign Key & Navigation for ProductVariant
        [Required(ErrorMessage = "Variant ID is required.")]
        [ForeignKey(nameof(ProductVariant))]
        public int variantId { get; set; } // Foreign Key - Calculated from ProductVariant

        public virtual ProductVariant ProductVariant { get; set; } // Navigation Property

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, 999, ErrorMessage = "Quantity must be between 1 and 999.")]
        public int quantity { get; set; } // User Input

        [Required(ErrorMessage = "Unit price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Unit price must be greater than 0.")]
       
        public decimal unitPrice { get; set; } // Calculated
    }
}
