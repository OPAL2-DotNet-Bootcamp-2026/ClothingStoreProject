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
            return context.ProductsVariant.ToList();
        }

        public ProductVariant GetById(int id)
        {
            return context.ProductsVariant.FirstOrDefault(v => v.variantId == id);
        }

        public List<ProductVariant> GetByProduct(int productId)
        {
            return context.ProductsVariant
                .Where(v => v.ProductId == productId)
                .ToList();
        }

        public ProductVariant GetBySku(string sku)
        {
            return context.ProductsVariant.FirstOrDefault(v => v.sku == sku);
        }

        public void Add(ProductVariant variant)
        {
            context.ProductsVariant.Add(variant);
            context.SaveChanges();
        }

        public void Update(ProductVariant variant)
        {
            context.SaveChanges();
        }

        public void Delete(ProductVariant variant)
        {
            context.ProductsVariant.Remove(variant);
            context.SaveChanges();
        }
    }
}
