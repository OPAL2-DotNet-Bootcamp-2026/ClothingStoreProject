using ClothingStore.DTOs;
using ClothingStore.Models;
using ClothingStore.Repos;

namespace ClothingStore.Services
{
    public class BrandService
    {
        private readonly BrandRepo brandRepo;
        private readonly ProductRepo productRepo;

        public BrandService(BrandRepo brandRepo, ProductRepo productRepo)
        {
            this.brandRepo = brandRepo;
            this.productRepo = productRepo;
        }

        public List<BrandResponseDto> GetAllBrands()
        {
            return brandRepo.GetAll().Select(MapToDto).ToList();
        }

        public BrandResponseDto? GetBrandById(int id)
        {
            Brand? brand = brandRepo.GetById(id);

            if (brand == null)
            {
                return null;
            }

            return MapToDto(brand);
        }

        public BrandResponseDto? AddBrand(CreateBrandDto dto)
        {
            string trimmedName = dto.brandName.Trim();

            if (brandRepo.NameExists(trimmedName))
            {
                return null;
            }

            Brand brand = new Brand
            {
                brandName = trimmedName,
                description = dto.description,
                logoUrl = dto.logoUrl,
                websiteUrl = dto.websiteUrl,
                isActive = true
            };

            brandRepo.Add(brand);

            return MapToDto(brand);
        }

        public BrandResponseDto? UpdateBrand(int id, UpdateBrandDto dto)
        {
            Brand? brand = brandRepo.GetById(id);

            if (brand == null)
            {
                return null;
            }

            if (!string.IsNullOrWhiteSpace(dto.brandName))
            {
                string trimmedName = dto.brandName.Trim();

                if (trimmedName != brand.brandName && brandRepo.NameExists(trimmedName))
                {
                    return null;
                }

                brand.brandName = trimmedName;
            }

            if (dto.description != null)
            {
                brand.description = dto.description;
            }

            if (dto.logoUrl != null)
            {
                brand.logoUrl = dto.logoUrl;
            }

            if (dto.websiteUrl != null)
            {
                brand.websiteUrl = dto.websiteUrl;
            }

            brandRepo.Update();

            return MapToDto(brand);
        }

        public bool DeactivateBrand(int id)
        {
            Brand? brand = brandRepo.GetById(id);

            if (brand == null)
            {
                return false;
            }

            brand.isActive = false;

            brandRepo.Update();

            return true;
        }

        public List<ProductListItemDto>? GetProductsByBrandId(int brandId)
        {
            Brand? brand = brandRepo.GetById(brandId);

            if (brand == null)
            {
                return null;
            }

            return productRepo.GetByBrand(brandId)
                .Select(MapToProductListItemDto)
                .ToList();
        }

        private BrandResponseDto MapToDto(Brand brand)
        {
            return new BrandResponseDto
            {
                brandId = brand.brandId,
                brandName = brand.brandName,
                description = brand.description,
                logoUrl = brand.logoUrl,
                websiteUrl = brand.websiteUrl,
                isActive = brand.isActive
            };
        }

        private ProductListItemDto MapToProductListItemDto(Product product)
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
    }
}