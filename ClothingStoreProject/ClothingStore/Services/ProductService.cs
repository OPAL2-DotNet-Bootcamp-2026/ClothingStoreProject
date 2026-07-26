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

        public List<ProductListItemDto> GetAllProducts()
        {
            return productRepo.GetAll()
                .Select(MapToListDto)
                .ToList();
        }

        private ProductListItemDto MapToListDto(Product product)
        {
            return new ProductListItemDto
            {
                productId = product.productId,
                productName = product.productName,
                basePrice = product.basePrice,
                BrandName = product.Brand.brandName,
                CategoryName = product.Category.categoryName,
                gender = product.gender,
                isAvailable = product.isAvailable
            };
        }

        private ProductDetailDto MapToDetailDto(Product product)
        {
            return new ProductDetailDto
            {
                productId = product.productId,
                productName = product.productName,
                description = product.description,
                basePrice = product.basePrice,
                BrandId = product.BrandId,
                BrandName = product.Brand.brandName,
                CategoryId = product.CategoryId,
                CategoryName = product.Category.categoryName,
                gender = product.gender,
                material = product.material,
                clothingStyle = product.clothingStyle,
                season = product.season,
                careInstructions = product.careInstructions,
                createdAt = product.createdAt,
                isAvailable = product.isAvailable,

                Variants = product.ProductVariants
                    .Select(v => new VariantSummaryDto
                    {
                        variantId = v.variantId,
                        size = v.size,
                        color = v.color,
                        price = v.price,
                        stockQuantity = v.stockQuantity,
                        imageUrl = v.imageUrl
                    })
                    .ToList(),

                AverageRating = product.Reviews.Any()
                    ? product.Reviews.Average(r => r.rating)
                    : 0,

                ReviewCount = product.Reviews.Count
            };
        }
    }
}
