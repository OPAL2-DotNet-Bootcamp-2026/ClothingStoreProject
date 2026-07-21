using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClothingStore.Models
{



    [Index(nameof(sku), IsUnique = true)]
    public class ProductVariant
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  int variantId { get; set; }//system generated


        [Required(ErrorMessage = "SKU is required.")]
        [MaxLength(100,ErrorMessage = "SKU cannot exceed 50 characters.")]
        public string  sku { get; set; }//user input 


        [Required(ErrorMessage = "Size is required.")]
        [MaxLength (10,ErrorMessage = "Size cannot exceed 10 characters.")]
        public  string  size { get; set; }//from  size list 


        [Required(ErrorMessage = "Color is required.") ]
        [MaxLength(30,ErrorMessage = "Color cannot exceed 30 characters.")]
        public string color { get; set; }//user input 


        [Required(ErrorMessage = "The price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = " Price must be greater than 0.")]
        public decimal price { get; set; }//user input 

        [Required(ErrorMessage = "Stock quantity is required.")]
        [Range(0, int.MaxValue,ErrorMessage = "Stock quantity must be greater than or equal to 0." )]
        public int stockQuantity { get; set; }//user input 


        [MaxLength(300,ErrorMessage = "Image URL cannot exceed 300 characters.")]
        public string? imageUrl { get; set; }//user input 


        [Required(ErrorMessage = "Product ID is required.")]////foreign  key to product
        [ForeignKey("product")]
        public int productId { get; set; }//from list 
        public  Product product { get; set; }// Navgation property



    }
}
