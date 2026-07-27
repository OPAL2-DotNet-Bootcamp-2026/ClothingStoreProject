using System.ComponentModel.DataAnnotations;

namespace ClothingStore.DTOs
{
    public class CategoryDTOs
    {
        public class BrandResponseDto
        {
            public int brandId { get; set; }
            public string brandName { get; set; }
            public string? description { get; set; }
            public string? logoUrl { get; set; }
            public string? websiteUrl { get; set; }
            public bool isActive { get; set; }
        }

        public class CreateBrandDto
        {
            [Required(ErrorMessage = "Brand Name Can't be Empty!!")]
            [MaxLength(100, ErrorMessage = "Brand Name Can't be more than 100 Character")]
            public string brandName { get; set; }

            [MaxLength(500, ErrorMessage = "description Can't be more than 500 Character")]
            public string? description { get; set; }

            [MaxLength(300, ErrorMessage = "logo Url Can't be more than 300 Character")]
            public string? logoUrl { get; set; }

            [MaxLength(200, ErrorMessage = "website Url Can't be more than 200 Character")]
            public string? websiteUrl { get; set; }
        }

        public class UpdateBrandDto
        {
            [MaxLength(100, ErrorMessage = "Brand Name Can't be more than 100 Character")]
            public string? brandName { get; set; }

            [MaxLength(500, ErrorMessage = "description Can't be more than 500 Character")]
            public string? description { get; set; }

            [MaxLength(300, ErrorMessage = "logo Url Can't be more than 300 Character")]
            public string? logoUrl { get; set; }

            [MaxLength(200, ErrorMessage = "website Url Can't be more than 200 Character")]
            public string? websiteUrl { get; set; }
        }
    }
}
