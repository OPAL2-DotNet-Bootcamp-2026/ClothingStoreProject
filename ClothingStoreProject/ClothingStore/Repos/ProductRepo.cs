using ClothingStore.Models;
using static ClothingStore.Enums;

namespace ClothingStore.Repos
{
    public class ProductRepo
    {
        private ClothingStoreContext context;

        public ProductRepo(ClothingStoreContext _context)
        {
            context = _context;
        }

        public List<Product> GetAll()
        {
            return context.products.ToList();
        }

        public Product GetById(int id)
        {
            return context.products.FirstOrDefault(p => p.productId == id);
        }

        public void Add(Product product)
        {
            context.products.Add(product);
            context.SaveChanges();
        }
    }
}
