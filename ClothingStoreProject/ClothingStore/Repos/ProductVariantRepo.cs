using ClothingStore.Models;

namespace ClothingStore.Repos
{
    public class ProductVariantRepo
    {
        private ClothingStoreContext context;

        public ProductVariantRepo(ClothingStoreContext _context)
        {
            context = _context;
        }

        public List<ProductVariant> GetAll()
        {
            return context.productsVariant.ToList();
        }
    }
}
