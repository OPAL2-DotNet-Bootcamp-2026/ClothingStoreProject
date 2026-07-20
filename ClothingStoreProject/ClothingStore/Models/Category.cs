using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Models
{
    [Index(nameof(categoryName), IsUnique = true)]

    public class Category
    {
                   // ── Primary Key ────────
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int categoryId { get; set; }             // system generated — يتولد تلقائي من قاعدة البيانات

                  // ── Required Fields ─────
        [Required]                                      // إجباري — ما يقبل فاضي
        [MaxLength(100)]                                // أقصى 100 حرف
        public string categoryName { get; set; }        // user input — اسم التصنيف مثل "Men's Shirts"

                 // ── Optional Fields ───────
        [MaxLength(500)]
        public string description { get; set; }         // user input — وصف التصنيف (اختياري)

        [MaxLength(300)]
        public string imageUrl { get; set; }            // user input — رابط صورة التصنيف (اختياري)

                 // ── Self-Referencing Foreign Key ──────

               public int? parentCategoryId { get; set; }      // from list — nullable (اختياري)

        // Navigation Property للـ Parent (الأب)
        // الـ ? يعني ممكن يكون null لو كانت top-level
        [ForeignKey("parentCategoryId")]
        public virtual Category ParentCategory { get; set; }   // navigation — التصنيف الأب

        // Navigation Property للـ Children (الأبناء)
        // كل category ممكن تكون أم لـ subcategories كثيرة
        public virtual ICollection<Category> SubCategories { get; set; } = new List<Category>();

        // Navigation Property للمنتجات التابعة لهذا التصنيف
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
