using ClothingStore.Models;


namespace ClothingStore.Repos
{
    public class BrandRepo
    {
        private ClothingStoreContext context;

        public BrandRepo(ClothingStoreContext _context)
        {
            context = _context;
        }

        public List<Brand> GetAll()
        {
            return context.brands.ToList();
        }

        public Brand GetById(int id)
        {
            return context.brands.FirstOrDefault(b => b.brandId == id);
        }

        public Brand GetByName(string name)
        {
            return context.brands.FirstOrDefault(b => b.brandName == name);
        }

        public bool NameExists(string name)
        {
            return context.brands.Any(b => b.brandName == name);
        }

        public void Add(Brand brand)
        {
            context.brands.Add(brand);
            context.SaveChanges();
        }

        public void Update()
        {
            context.SaveChanges();
        }

        public void Delete(Brand brand)
        {
            context.brands.Remove(brand);
            context.SaveChanges();

        }
    }
}
