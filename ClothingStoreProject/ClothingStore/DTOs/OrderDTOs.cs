using System.ComponentModel.DataAnnotations;
using static ClothingStore.Enums;

namespace ClothingStore.DTOs
{
    public class OrderDTOs
    {
        // 1. Used when returning an order and its items back to the client
        public class OrderResponseDto
        {
            public int OrderId { get; set; }
            public int UserId { get; set; }
            public DateTime OrderDate { get; set; }
            public decimal TotalAmount { get; set; } // Computed server-side by OrderService
            public OrderStatus Status { get; set; }
            public string ShippingAddress { get; set; }
        
            // OrderItems nested inside the order response DTO
            public List<OrderItemResponseDto> OrderItems { get; set; } = new();
        }

        // 2. Used when returning individual historical items inside an order
        public class OrderItemResponseDto
        {
            public int OrderItemId { get; set; }
            public int VariantId { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; } // Price snapshot at the exact moment of purchase
        }

        // 3. Used when a user checks out from their cart to create an order
        public class CreateOrderDto
        {
            [Required(ErrorMessage = "Shipping address is required.")]
            [StringLength(300, ErrorMessage = "Shipping address cannot exceed 300 characters.")]
            public string ShippingAddress { get; set; } // Client Input
        }

        // 4. Used by an Admin to update the status of an order
        public class UpdateOrderStatusDto
        {
            [Required(ErrorMessage = "Status is required.")]
            public OrderStatus Status { get; set; } // Admin Input
        }
    }
}
