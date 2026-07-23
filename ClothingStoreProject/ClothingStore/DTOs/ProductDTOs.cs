using System.ComponentModel.DataAnnotations;
using static ClothingStore.Enums;

namespace ClothingStore.DTOs
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "Product Name is Required")]
        [MaxLength(150, ErrorMessage = "Product Name Can't be more the 150 Characters")]
        public string productName { get; set; }

        [MaxLength(1000, ErrorMessage = "Description Can't be more the 1000 Characters")]
        public string? description { get; set; }

        [Required(ErrorMessage = "Base price is Required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Base price must be greater than 0.")]
        public decimal basePrice { get; set; }

        [Required(ErrorMessage = "The brand Id is Required")]
        public int BrandId { get; set; }

        [Required(ErrorMessage = "The category Id is Required")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Gender is Required")]
        public Gender gender { get; set; }

        [MaxLength(100, ErrorMessage = "Material Can't be more the 100 Characters")]
        public string? material { get; set; }

        [Required(ErrorMessage = "ClothingStyle Can't be empty")]
        public ClothingStyle clothingStyle { get; set; }

        public Season? season { get; set; }

        [MaxLength(300, ErrorMessage = "Care Instructions Can't be more the 300 Characters")]
        public string? careInstructions { get; set; }
    }
}
