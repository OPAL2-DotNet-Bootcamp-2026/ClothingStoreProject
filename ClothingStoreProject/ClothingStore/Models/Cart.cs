using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static ClothingStore.Enums;

namespace ClothingStore.Models
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int cartId { get; set; } // System Generated

        [Required(ErrorMessage = "User ID is required.")]
        [ForeignKey(nameof(User))]
        public int userId { get; set; } // From User List / System
        public virtual User User { get; set; } // Navigation Property

        [Required(ErrorMessage = "Creation date is required.")]
        public DateTime createdAt { get; set; } = DateTime.UtcNow; // System Calculated

        [Required(ErrorMessage = "Update date is required.")]
        public DateTime updatedAt { get; set; } = DateTime.UtcNow; // System Calculated

        // Navigation property for Cart Items
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>(); // Navigation Property
    }
}