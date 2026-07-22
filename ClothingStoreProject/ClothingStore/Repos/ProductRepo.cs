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

        public void Update()
        {
            context.SaveChanges();
        }

        public void Delete(Product product)
        {
            context.products.Remove(product);
            context.SaveChanges();
        }

        public Product GetByName(string name)
        {
            return context.products.FirstOrDefault(p => p.productName == name);
        }
    }
}
