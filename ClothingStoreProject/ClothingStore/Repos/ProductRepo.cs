using ClothingStore.Models;
using Microsoft.EntityFrameworkCore;
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
            return context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .ToList();
        }

        public Product? GetById(int id)
        {
            return context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Include(p => p.ProductVariants)
                .Include(p => p.Reviews)
                .FirstOrDefault(p => p.productId == id);
        }

        public void Add(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }

        public void Update()
        {
            context.SaveChanges();
        }

        public void Delete(Product product)
        {
            context.Products.Remove(product);
            context.SaveChanges();
        }

        public List<Product> SearchByName(string name)
        {
            return context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Where(p => p.productName.Contains(name))
                .ToList();
        }

        public List<Product> GetByBrand(int brandId)
        {
            return context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Where(p => p.BrandId == brandId)
                .ToList();
        }

        public List<Product> GetByCategory(int categoryId)
        {
            return context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId)
                .ToList();

        }

        public List<Product> GetByGender(Gender gender)
        {
            return context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Where(p => p.gender == gender)
                .ToList();
        }

        public List<Product> GetAvailable()
        {
            return context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .Where(p => p.isAvailable)
                .ToList();
        }

        public bool NameExists(string name)
        {
            return context.Products
                .Any(p => p.productName == name);
        }
    }
}
