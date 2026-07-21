using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace ClothingStore.Models
    
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int cartId { get; set; }

        [Required]
        public int userId { get; set; }

        // Keeping one active cart per user as per specs
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

        [Required]
        public DateTime createdAt { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime updatedAt { get; set; } = DateTime.UtcNow;
    }
}
