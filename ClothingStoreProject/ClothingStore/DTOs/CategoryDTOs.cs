using System.ComponentModel.DataAnnotations;

namespace ClothingStore.DTOs
{
    public class CategoryDTOs
    {
        public int categoryId {get; set;}
        public string categoryName {get; set;}
        public string? description {get; set;}
        public string? imageUrl {get; set;}
        public int? parentCategoryId {get; set;}
    }

    public class CreateCategoryDto
    {
        [Required(ErrorMessage = "category Name Can't be Empty!!")]
        [MaxLength(100, ErrorMessage = "category Name Can't be more than 100 Character")]
        public string categoryName {get; set;}

        [MaxLength(500, ErrorMessage = "description Can't be more than 500 Character")]
        public string? description {get; set;}

        [MaxLength(300, ErrorMessage = "image Url Can't be more than 300 Character")]
        public string? imageUrl {get; set;}

        public int? parentCategoryId {get; set;}
    }

    public class UpdateCategoryDto
    {
        [MaxLength(100, ErrorMessage = "category Name Can't be more than 100 Character")]
        public string? categoryName {get; set;}

        [MaxLength(500, ErrorMessage = "description Can't be more than 500 Character")]
        public string? description {get; set;}

        [MaxLength(300, ErrorMessage = "image Url Can't be more than 300 Character")]
        public string? imageUrl {get; set;}

        public int? parentCategoryId {get; set;}

    }
}
