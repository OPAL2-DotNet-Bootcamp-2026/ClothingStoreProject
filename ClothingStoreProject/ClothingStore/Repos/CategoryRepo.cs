using ClothingStore.Models;

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
            return context.categories.ToList();
        }

        public Category GetById(int id)
        {
            return context.categories.FirstOrDefault(c => c.categoryId == id);
        }

        public Category GetByName(string name)
        {
            return context.categories.FirstOrDefault(c => c.categoryName == name);
        }

        public bool NameExists(string name)
        {
            return context.categories.Any(c => c.categoryName == name);
        }

        public void Add(Category category)
        {
            context.categories.Add(category);
            context.SaveChanges();
        }

        public void Update()
        {
            context.SaveChanges();
        }

        public void Delete(Category category)
        {
            context.categories.Remove(category);
            context.SaveChanges();

        }
    }
}
