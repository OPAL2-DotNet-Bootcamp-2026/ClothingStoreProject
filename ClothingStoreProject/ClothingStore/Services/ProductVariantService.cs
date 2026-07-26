using ClothingStore.Repos;

namespace ClothingStore.Services
{
    public class ProductVariantService
    {
        private ProductVariantRepo productVariantRepo;
        private ProductRepo productRepo;

        public ProductVariantService(
            ProductVariantRepo _productVariantRepo,
            ProductRepo _productRepo)
        {
            productVariantRepo = _productVariantRepo;
            productRepo = _productRepo;
        }
    }
}
