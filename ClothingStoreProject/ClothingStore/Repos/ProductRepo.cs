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

    }
}
