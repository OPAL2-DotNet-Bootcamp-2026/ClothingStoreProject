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


        public VariantResponseDto? GetVariantById(int id)
        {
            ProductVariant? variant = productVariantRepo.GetById(id);

            if (variant == null)
            {
                return null;
            }

            return MapToResponseDto(variant);
        }

        public List<VariantResponseDto> GetVariantsByProduct(int productId)
        {
            List<ProductVariant> variants = productVariantRepo.GetByProduct(productId);

            return variants
                .Select(variant => MapToResponseDto(variant))
                .ToList();
        }

        public VariantResponseDto? GetVariantBySku(string sku)
        {
            ProductVariant? variant = productVariantRepo.GetBySku(sku);

            if (variant == null)
            {
                return null;
            }

            return MapToResponseDto(variant);
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
