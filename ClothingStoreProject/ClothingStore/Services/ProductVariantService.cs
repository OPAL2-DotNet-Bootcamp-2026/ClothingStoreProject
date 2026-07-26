using ClothingStore.DTOs;
using ClothingStore.Models;
using ClothingStore.Repos;
using static ClothingStore.Enums;

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

        public VariantResponseDto? AddVariant(ProductVariantDTOs dto)
        {
            Product? product = productRepo.GetById(dto.ProductId);

            if (product == null)
            {
                return null;
            }

            ProductVariant? existingVariant = productVariantRepo.GetBySku(dto.sku);

            if (existingVariant != null)
            {
                return null;
            }

            ProductVariant variant = new ProductVariant
            {
                ProductId = dto.ProductId,
                Product = product,
                sku = dto.sku,
                size = dto.size,
                color = dto.color,
                price = dto.price,
                stockQuantity = dto.stockQuantity,
                imageUrl = dto.imageUrl
            };

            productVariantRepo.Add(variant);

            return MapToResponseDto(variant);
        }

        public VariantResponseDto? UpdateVariant(int id,UpdateVariantDto dto)
        {
            ProductVariant? variant = productVariantRepo.GetById(id);

            if (variant == null)
            {
                return null;
            }

            if (dto.size.HasValue)
            {
                variant.size = dto.size.Value;
            }

            if (!string.IsNullOrWhiteSpace(dto.color))
            {
                variant.color = dto.color;
            }

            if (dto.price.HasValue)
            {
                variant.price = dto.price.Value;
            }

            if (dto.imageUrl != null)
            {
                variant.imageUrl = dto.imageUrl;
            }

            productVariantRepo.Update(variant);

            return MapToResponseDto(variant);
        }

        public VariantSummaryDto MapToSummaryDto(ProductVariant variant)
        {
            return new VariantSummaryDto
            {
                variantId = variant.variantId,
                size = variant.size,
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
