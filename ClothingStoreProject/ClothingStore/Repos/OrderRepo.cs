using ClothingStore.Models;
using Microsoft.EntityFrameworkCore;
using static ClothingStore.Enums;

namespace ClothingStore.Repos
{
    public class OrderRepo
    {
        private ClothingStoreContext context;

        public OrderRepo(ClothingStoreContext _context)
        {
            context = _context;
        }

        public List<Order> GetAll()
        {
            return context.Orders.ToList();
        }

        public Order? GetById(int id)
        {
            return context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefault(o => o.orderId == id);
        }

        public List<Order> GetByUserId(int userId)
        {
            return context.Orders.Where(o => o.userId == userId).ToList();
        }

        public List<Order> GetByStatus(OrderStatus status)
        {
            return context.Orders.Where(o => o.status == status).ToList();
        }

        public Order Add(Order order)
        {
            context.Orders.Add(order);
            context.SaveChanges();
            return order;
        }

        public void Update()
        {
            context.SaveChanges();
        }
    }
}