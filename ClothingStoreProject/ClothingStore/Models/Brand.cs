using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Models
{
    [Index(nameof(brandName), IsUnique = true)]
    public class Brand
    {
                  // ── Primary Key ─────────
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int brandId { get; set; }                // system generated — الرقم يتولد تلقائي من قاعدة البيانات

        // ── Required Fields ─────
        [Required(ErrorMessage = "Brand Name Can't be Empty!!")]                         // إجباري — ما يقبل فاضي
        [MaxLength(100, ErrorMessage = "Brand Name Can't be more than 100 Character")]  // أقصى طول 100 حرف
        public string brandName { get; set; }           // user input — اسم البراند

                  // ── Optional Fields ─────
        [MaxLength(500, ErrorMessage = "description Can't be more than 500 Character")]                                // أقصى طول 500 حرف
        public string? description { get; set; }         // user input — وصف البراند (اختياري)

        [MaxLength(300, ErrorMessage = "logo Url Can't be more than 100 Character")]                                // أقصى طول 300 حرف
        public string? logoUrl { get; set; }             // user input — رابط صورة اللوجو (اختياري)

        [MaxLength(200, ErrorMessage = "website Url Can't be more than 100 Character")]                                // أقصى طول 200 حرف
        public string? websiteUrl { get; set; }          // user input — رابط الموقع (اختياري)

                 // ── Default Value ───────
        public bool isActive { get; set; } = true;      // default value — يكون true تلقائياً عند الإضافة 

               // ── Navigation Property ───────
               // علاقة 1:M — كل Brand عندها منتجات كثيرة
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
