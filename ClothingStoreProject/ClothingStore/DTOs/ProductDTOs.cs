using System.ComponentModel.DataAnnotations;

namespace ClothingStore.DTOs
{
    public class CreateVariantDto
    {
        [Required(ErrorMessage = "Product Id is Required")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Sku is Required")]
        [MaxLength(50, ErrorMessage = "Sku Can't be more the 50 Characters")]
        public string sku { get; set; }

        [Required(ErrorMessage = "Size is Required")]
        [MaxLength(10, ErrorMessage = "Size Can't be more the 10 Characters")]
        public string size { get; set; }

        [Required(ErrorMessage = "Color is Required")]
        [MaxLength(30, ErrorMessage = "Color Can't be more the 30 Characters")]
        public string color { get; set; }

        [Required(ErrorMessage = "Price is Required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity can't be negative.")]
        public int stockQuantity { get; set; } = 0;

        [MaxLength(300, ErrorMessage = "Image Url Can't be more the 300 Characters")]
        public string? imageUrl { get; set; }
    }
}
