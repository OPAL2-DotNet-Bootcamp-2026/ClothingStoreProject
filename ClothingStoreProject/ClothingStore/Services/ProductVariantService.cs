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

        public List<VariantResponseDto> GetAllVariants()
        {
            List<ProductVariant> variants = productVariantRepo.GetAll();
            return variants
                .Select(variant => MapToResponseDto(variant))
                .ToList();
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

        private VariantResponseDto MapToResponseDto(ProductVariant variant)
        {
            return new VariantResponseDto
            {
                variantId = variant.variantId,
                ProductId = variant.ProductId,
                sku = variant.sku,
                size = variant.size.ToString(),
                color = variant.color,
                price = variant.price,
                stockQuantity = variant.stockQuantity,
                imageUrl = variant.imageUrl
            };
        }
    }
}
