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
    }
}
