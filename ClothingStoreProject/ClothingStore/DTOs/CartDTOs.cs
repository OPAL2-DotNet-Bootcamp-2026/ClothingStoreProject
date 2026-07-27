using System.ComponentModel.DataAnnotations;

namespace ClothingStore.DTOs
{
    // 1. Used when returning the full cart to the client
    public class CartResponseDto
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // CartItems nested neatly inside the response DTO 
        public List<CartItemResponseDto> CartItems { get; set; } = new();
    }

    // 2. Used when returning individual item details inside the cart
    public class CartItemResponseDto
    {
        public int CartItemId { get; set; }
        public int VariantId { get; set; }
        public string ProductName { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }
    }

    // 3. Used when the user sends a request to add an item
    public class AddCartItemDto
    {
        [Required(ErrorMessage = "Variant ID is required.")]
        public int VariantId { get; set; } // Client Input: Specifies which product variant to add

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, 999, ErrorMessage = "Quantity must be between 1 and 999.")]
        public int Quantity { get; set; } // User Input: Specifies how many units to add
    }

    // 4. Used when updating an existing item's quantity in the cart
    public class UpdateCartItemDto
    {
        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, 999, ErrorMessage = "Quantity must be between 1 and 999.")]
        public int Quantity { get; set; } // User Input: Specifies the new quantity for an existing cart item
    }
}