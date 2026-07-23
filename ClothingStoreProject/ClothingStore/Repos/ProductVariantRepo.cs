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

        public ProductVariant GetById(int id)
        {
            return context.productsVariant.FirstOrDefault(v => v.variantId == id);
        }

        public List<ProductVariant> GetByProduct(int productId)
        {
            return context.productsVariant
                .Where(v => v.ProductId == productId)
                .ToList();
        }
    }
}
