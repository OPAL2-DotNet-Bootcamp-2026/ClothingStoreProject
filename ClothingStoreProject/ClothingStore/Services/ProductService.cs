using ClothingStore.DTOs;
using ClothingStore.Models;
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


        private ProductListItemDto MapToListDto(Product product)
        {
            return new ProductListItemDto
            {
                productId = product.productId,
                productName = product.productName,
                basePrice = product.basePrice,
                BrandName = product.Brand?.brandName ?? "Unknown",
                CategoryName =
                    product.Category?.categoryName ?? "Unknown",
                gender = product.gender,
                isAvailable = product.isAvailable
            };
        }
    }
}
