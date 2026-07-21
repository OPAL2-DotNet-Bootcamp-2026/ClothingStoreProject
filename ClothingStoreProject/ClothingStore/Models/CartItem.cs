using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace ClothingStore.Models
{
    public class CartItem
    {
        public int cartItemId { get; set; }
        public int cartId { get; set; }
        public int variantId { get; set; }
        public int quantity { get; set; }
        public DateTime addedAt { get; set; }
    }
}
