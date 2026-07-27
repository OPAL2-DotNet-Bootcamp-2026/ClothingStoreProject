using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Models
{
    [Index(nameof(categoryName), IsUnique = true)]

    public class Category
    {
                
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int categoryId { get; set; }             // system generated 

      
        [Required(ErrorMessage = "category Name Can't be Empty!!")]                        
        [MaxLength(100, ErrorMessage = "category Name Can't be more than 100 Character")]  
        public string categoryName { get; set; }        // user input  

                 // ── Optional Fields ───────
        [MaxLength(500, ErrorMessage = "description  Can't be more than 500 Character")]
        public string? description { get; set; }         // user input 

        [MaxLength(300, ErrorMessage = "image Url Can't be more than 300 Character")]
        public string? imageUrl { get; set; }            // user input

        // ── Self-Referencing Foreign Key ──────

        [ForeignKey("ParentCategory")]
        public int? parentCategoryId { get; set; }      // from list — nullable
      
        public virtual Category? ParentCategory { get; set; }   // navigation 

     
        public virtual ICollection<Category> SubCategories { get; set; } = new List<Category>();

        // Navigation Property 
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
