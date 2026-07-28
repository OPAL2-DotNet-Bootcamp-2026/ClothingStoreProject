using  ClothingStore.DTOs;
using ClothingStore.Models;
using ClothingStore.Repos;
using static ClothingStore.DTOs.OrderDTOs;
using static ClothingStore.Enums;

namespace ClothingStore.Services


{
    public class OrderService
    {
        private OrderRepo orderRepo;
        private OrderItemRepo orderItemRepo;
        private CartRepo cartRepo;
        private CartItemRepo cartItemRepo;
        private ProductVariantRepo variantRepo;

        public OrderService(OrderRepo _orderRepo, OrderItemRepo _orderItemRepo, CartRepo _cartRepo,
            CartItemRepo _cartItemRepo, ProductVariantRepo _variantRepo) //constructer 
        {
            orderRepo = _orderRepo;
            orderItemRepo = _orderItemRepo;
            cartRepo = _cartRepo;
            cartItemRepo = _cartItemRepo;
            variantRepo = _variantRepo;
        }
        public List<OrderResponseDto> GetAll()
        {
            return orderRepo.GetAll().Select(MapToListItemDto).ToList();
        }

        public List<OrderResponseDto> GetByUserId(int userId)
        {
            return orderRepo.GetByUserId(userId).Select(MapToListItemDto).ToList();
        }

        public OrderResponseDto? GetById(int id)
        {
            var order = orderRepo.GetById(id);
            return order == null ? null : MapToDetailDto(order);
        }

        public OrderResponseDto? Checkout(int userId, CreateOrderDto dto)
        {
            var cart = cartRepo.GetByUserId(userId);
            if (cart == null || !cart.CartItems.Any())
                return null; // Controller translates -> BadRequest("Cart is empty.")

            var variantIds = cart.CartItems.Select(ci => ci.variantId).ToList();
            var variants = variantIds.ToDictionary(id => id, id => variantRepo.GetById(id));

            foreach (var cartItem in cart.CartItems)
            {
                var variant = variants[cartItem.variantId];
                if (variant == null || variant.stockQuantity < cartItem.quantity)
                    return null; // Controller translates -> BadRequest("Item out of stock: ...")
            }

            var order = new Order
            {
                userId = userId,
                orderDate = DateTime.UtcNow,
                status = OrderStatus.Pending,
                shippingAddress = dto.ShippingAddress,
                totalAmount = 0
            };
            order = orderRepo.Add(order); // Saved once here so orderId exists for the items below

            decimal total = 0;
            foreach (var cartItem in cart.CartItems)
            {
                var variant = variants[cartItem.variantId];

                var orderItem = new OrderItem
                {
                    orderId = order.orderId,
                    variantId = cartItem.variantId,
                    quantity = cartItem.quantity,
                    unitPrice = variant.price // Snapshot at time of purchase
                };
                orderItemRepo.Add(orderItem);

                variant.stockQuantity -= cartItem.quantity;
                total += variant.price * cartItem.quantity;
            }

            order.totalAmount = total;
            orderRepo.Update(); // Single SaveChanges: OrderItems, stock changes, and totalAmount together

            cartItemRepo.ClearCart(cart.cartId);

            return GetById(order.orderId);
        }

        public OrderResponseDto? UpdateStatus(int id, UpdateOrderStatusDto dto)
        {
            var order = orderRepo.GetById(id);
            if (order == null)
                return null;

            order.status = dto.Status;
            orderRepo.Update();
            return MapToDetailDto(order);
        }

        private OrderResponseDto MapToListItemDto(Order order)
        {
            return new OrderResponseDto
            {
                OrderId = order.orderId,
                UserId = order.userId,
                OrderDate = order.orderDate,
                TotalAmount = order.totalAmount,
                Status = order.status
            };
        }
        private OrderResponseDto MapToDetailDto(Order order)
        {
            return new OrderResponseDto
            {
                OrderId = order.orderId,
                UserId = order.userId,
                OrderDate = order.orderDate,
                TotalAmount = order.totalAmount,
                Status = order.status,
                ShippingAddress = order.shippingAddress,
                OrderItems = order.OrderItems.Select(oi => new OrderItemResponseDto
                {
                    OrderItemId = oi.orderItemId,
                    VariantId = oi.variantId,
                    Quantity = oi.quantity,
                    UnitPrice = oi.unitPrice
                }).ToList()
            };
        }
    }
}
