using ClothingStore.Models;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Repos
{
    public class CategoryRepo
    {
        private ClothingStoreContext context;

        public CategoryRepo(ClothingStoreContext _context)
        {
            context = _context;
        }

        public List<Category> GetAll()
        {
            return context.Categories.ToList();
        }

        public Category? GetById(int id)
        {
            return context.Categories.FirstOrDefault(c => c.categoryId == id);
        }

        public Category? GetByName(string name)
        {
            return context.Categories.FirstOrDefault(c => c.categoryName == name);
        }

        public bool NameExists(string name)
        {
            return context.Categories.Any(c => c.categoryName == name);
        }

        public bool NameExists(string name, int excludeCategoryId)
        {
            return context.Categories.Any(c => c.categoryName == name && c.categoryId != excludeCategoryId);
        }

        public List<Category> GetSubcategories(int parentId)
        {
            return context.Categories.Where(c => c.parentCategoryId == parentId).ToList();
        }

        public List<Category> GetTopLevelCategories()
        {
            return context.Categories.Where(c => c.parentCategoryId == null).ToList();
        }

        public void Add(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
        }

        public void Update()
        {
            context.SaveChanges();
        }
    }
}