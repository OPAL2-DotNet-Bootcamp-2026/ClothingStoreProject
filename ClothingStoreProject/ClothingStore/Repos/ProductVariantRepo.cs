namespace ClothingStore.Repos
{
    public class ProductVariantRepo
    {
        private ClothingStoreContext context;

        public ProductVariantRepo(ClothingStoreContext _context)
        {
            context = _context;
        }
    }
}
