using ClothingStore.DTOs;
using ClothingStore.Models;
using ClothingStore.Repos;

namespace ClothingStore.Services
{
    public class CategoryService
    {
        private readonly CategoryRepo categoryRepo;
        private readonly ProductRepo productRepo;

        public CategoryService(CategoryRepo categoryRepo, ProductRepo productRepo)
        {
            this.categoryRepo = categoryRepo;
            this.productRepo = productRepo;
        }

        public List<CategoryResponseDto> GetAllCategories()
        {
            return categoryRepo.GetAll()
                .Select(MapToDto)
                .ToList();
        }

        public CategoryResponseDto? GetCategoryById(int id)
        {
            Category? category = categoryRepo.GetById(id);

            if (category == null)
            {
                return null;
            }

            return MapToDto(category);
        }

        public List<CategoryResponseDto> GetTopLevelCategories()
        {
            return categoryRepo.GetTopLevelCategories()
                .Select(MapToDto)
                .ToList();
        }

        public List<CategoryResponseDto>? GetSubcategories(int parentId)
        {
            Category? parent = categoryRepo.GetById(parentId);

            if (parent == null)
            {
                return null;
            }

            return categoryRepo.GetSubcategories(parentId)
                .Select(MapToDto)
                .ToList();
        }

        public CategoryResponseDto? AddCategory(CreateCategoryDto dto)
        {
            string trimmedName = dto.categoryName.Trim();

            if (categoryRepo.NameExists(trimmedName))
            {
                return null;
            }

            if (dto.parentCategoryId.HasValue)
            {
                Category? parent = categoryRepo.GetById(dto.parentCategoryId.Value);

                if (parent == null)
                {
                    return null;
                }
            }

            Category category = new Category
            {
                categoryName = trimmedName,
                description = dto.description,
                imageUrl = dto.imageUrl,
                parentCategoryId = dto.parentCategoryId,
                isActive = true
            };

            categoryRepo.Add(category);

            return MapToDto(category);
        }

        public CategoryResponseDto? UpdateCategory(int id, UpdateCategoryDto dto)
        {
            Category? category = categoryRepo.GetById(id);

            if (category == null)
            {
                return null;
            }

            if (!string.IsNullOrWhiteSpace(dto.categoryName))
            {
                string trimmedName = dto.categoryName.Trim();

                if (trimmedName != category.categoryName && categoryRepo.NameExists(trimmedName))
                {
                    return null;
                }

                category.categoryName = trimmedName;
            }

            if (dto.description != null)
            {
                category.description = dto.description;
            }

            if (dto.imageUrl != null)
            {
                category.imageUrl = dto.imageUrl;
            }

          
            if (dto.removeParentCategory)
            {
                category.parentCategoryId = null;
            }
            else if (dto.parentCategoryId.HasValue)
            {
                int newParentId = dto.parentCategoryId.Value;

                if (newParentId == category.categoryId)
                {
                    return null;
                }

                Category? parent = categoryRepo.GetById(newParentId);

                if (parent == null)
                {
                    return null;
                }

                if (WouldCreateCycle(category.categoryId, newParentId))
                {
                    return null;
                }

                category.parentCategoryId = newParentId;
            }

            categoryRepo.Update();

            return MapToDto(category);
        }


        public bool? DeactivateCategory(int id)
        {
            Category? category = categoryRepo.GetById(id);

            if (category == null)
            {
                return null;
            }

            bool hasActiveProducts = productRepo.GetByCategory(id).Any(p => p.isAvailable);

            if (hasActiveProducts)
            {
                return false;
            }

            category.isActive = false;

            categoryRepo.Update();

            return true;
        }

        //public List<ProductListItemDto>? GetProductsByCategoryId(int categoryId)
        //{
        //    Category? category = categoryRepo.GetById(categoryId);

        //    if (category == null)
        //    {
        //        return null;
        //    }

        //    return productRepo.GetByCategory(categoryId)
        //        .Select(MapToProductListItemDto)
        //        .ToList();
        //}

        private bool WouldCreateCycle(int categoryId, int newParentId)
        {
            var visited = new HashSet<int>();
            int? currentId = newParentId;

            while (currentId.HasValue)
            {
                if (currentId.Value == categoryId)
                {
                    return true;
                }

                if (!visited.Add(currentId.Value))
                {
                    return true;
                }

                Category? current = categoryRepo.GetById(currentId.Value);
                currentId = current?.parentCategoryId;
            }

            return false;
        }

        private CategoryResponseDto MapToDto(Category category)
        {
            return new CategoryResponseDto
            {
                categoryId = category.categoryId,
                categoryName = category.categoryName,
                description = category.description,
                imageUrl = category.imageUrl,
                parentCategoryId = category.parentCategoryId,
                isActive = category.isActive
            };
        }

        //private ProductListItemDto MapToProductListItemDto(Product product)
        //{
        //    return new ProductListItemDto
        //    {
        //        productId = product.productId,
        //        productName = product.productName,
        //        basePrice = product.basePrice,
        //        BrandName = product.Brand.brandName,
        //        CategoryName = product.Category.categoryName,
        //        gender = product.gender,
        //        isAvailable = product.isAvailable
        //    };
        //}
    }
}