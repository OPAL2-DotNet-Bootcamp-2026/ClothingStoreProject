using ClothingStore.Models;

namespace ClothingStore.Repos
{
    public class OrderItemRepo
    {
        private ClothingStoreContext context;

        public OrderItemRepo(ClothingStoreContext _context)
        {
            context = _context;
        }

        public List<OrderItem> GetByOrderId(int orderId)
        {
            return context.OrderItems.Where(oi => oi.orderId == orderId).ToList();
        }

        public void Add(OrderItem orderItem)
        {
            context.OrderItems.Add(orderItem);
        }

        // No SaveChanges here — OrderService.Checkout() saves once,
        // as part of the same transaction as the Order and stock updates (§11).
    }
}