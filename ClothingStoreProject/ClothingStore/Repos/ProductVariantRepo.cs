using ClothingStore.Models;
using Microsoft.EntityFrameworkCore;

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
            return context.ProductsVariant
                .Include(v => v.Product)
                .ToList();
        }

        public ProductVariant? GetById(int id)
        {
            return context.ProductsVariant
                .Include(v => v.Product)
                .FirstOrDefault(v => v.variantId == id);
        }

        public List<ProductVariant> GetByProduct(int productId)
        {
            return context.ProductsVariant
                .Include(v => v.Product)
                .Where(v => v.ProductId == productId)
                .ToList();
        }

        public ProductVariant? GetBySku(string sku)
        {
            return context.ProductsVariant
                .Include(v => v.Product)
                .FirstOrDefault(v => v.sku == sku);
        }

        public void Add(ProductVariant variant)
        {
            context.ProductsVariant.Add(variant);
            context.SaveChanges();
        }

        public void Update()
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
