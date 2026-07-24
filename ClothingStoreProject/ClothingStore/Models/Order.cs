using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static ClothingStore.Enums;

namespace ClothingStore.Models
{
    public class Order
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int orderId { get; set; } // System Generated

        [Required(ErrorMessage = "User ID is required.")]
        [ForeignKey(nameof(User))]
        public int userId { get; set; } // From User List / System
        public virtual User User { get; set; } // Navigation Property

        [Required(ErrorMessage = "Order date is required.")]
        public DateTime orderDate { get; set; } = DateTime.UtcNow; // System Calculated

        [Required(ErrorMessage = "Total amount is required.")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Total amount must be 0 or greater.")]
        public decimal totalAmount { get; set; } // System Calculated / User Input

        [Required(ErrorMessage = "Status is required.")]
        [MaxLength(30, ErrorMessage = "Status cannot exceed 30 characters.")]
        public string status { get; set; } = "Pending"; // System Calculated / User Input

        [Required(ErrorMessage = "Shipping address is required.")]
        [MaxLength(300, ErrorMessage = "Shipping address cannot exceed 300 characters.")]
        public string shippingAddress { get; set; } // User Input

        // Navigation property for Order Items
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>(); // Navigation Property
    }
}