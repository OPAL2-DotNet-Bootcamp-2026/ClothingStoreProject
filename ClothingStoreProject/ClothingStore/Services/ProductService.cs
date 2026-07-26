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

        public ProductDetailDto? GetProductById(int id)
        {
            Product? product = productRepo.GetById(id);

            if (product == null)
            {
                return null;
            }

            return MapToDetailDto(product);
        }

        public ProductDetailDto? UpdateProduct(int id, UpdateProductDto dto)
        {
            Product? product = productRepo.GetById(id);

            if (product == null)
            {
                return null;
            }

            if (!string.IsNullOrWhiteSpace(dto.productName))
            {
                if (dto.productName != product.productName &&
                    productRepo.NameExists(dto.productName))
                {
                    return null;
                }

                product.productName = dto.productName;
            }

            if (dto.description != null)
            {
                product.description = dto.description;
            }

            if (dto.basePrice.HasValue)
            {
                product.basePrice = dto.basePrice.Value;
            }

            if (dto.BrandId.HasValue)
            {
                Brand? brand = brandRepo.GetById(dto.BrandId.Value);

                if (brand == null)
                {
                    return null;
                }

                product.BrandId = dto.BrandId.Value;
                product.Brand = brand;
            }

            if (dto.CategoryId.HasValue)
            {
                Category? category = categoryRepo.GetById(dto.CategoryId.Value);

                if (category == null)
                {
                    return null;
                }

                product.CategoryId = dto.CategoryId.Value;
                product.Category = category;
            }

            if (dto.gender.HasValue)
            {
                product.gender = dto.gender.Value;
            }

            if (dto.material != null)
            {
                product.material = dto.material;
            }

            if (dto.clothingStyle.HasValue)
            {
                product.clothingStyle = dto.clothingStyle.Value;
            }

            if (dto.season.HasValue)
            {
                product.season = dto.season.Value;
            }

            if (dto.careInstructions != null)
            {
                product.careInstructions = dto.careInstructions;
            }

            productRepo.Update();

            return MapToDetailDto(product);
        }

        public bool DeactivateProduct(int id)
        {
            Product? product = productRepo.GetById(id);

            if (product == null)
            {
                return false;
            }

            product.isAvailable = false;

            productRepo.Update();

            return true;
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
