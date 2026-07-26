using ClothingStore.Repos;

namespace ClothingStore.Services
{
    public class ProductService
    {
        private readonly ProductRepo productRepo;
        private readonly BrandRepo brandRepo;
        private readonly CategoryRepo categoryRepo;

        public ProductService(
            ProductRepo productRepo,
            BrandRepo brandRepo,
            CategoryRepo categoryRepo)
        {
            this.productRepo = productRepo;
            this.brandRepo = brandRepo;
            this.categoryRepo = categoryRepo;
        }
    }
}
