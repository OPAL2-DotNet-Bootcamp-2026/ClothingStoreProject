using ClothingStore.DTOs;
using ClothingStore.Models;
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
        public VariantSummaryDto MapToSummaryDto(ProductVariant variant)
        {
            return new VariantSummaryDto
            {
                variantId = variant.variantId,
                size = variant.size.ToString(),
                color = variant.color,
                price = variant.price,
                stockQuantity = variant.stockQuantity
            };
        }
    }
}
