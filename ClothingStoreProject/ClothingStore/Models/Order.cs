namespace ClothingStore.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int orderId { get; set; }

        [Required]
        public int userId { get; set; }

        [Required]
        public DateTime orderDate { get; set; } = DateTime.UtcNow;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue)]
        public decimal totalAmount { get; set; }

        [Required]
        [MaxLength(30)]
        public string status { get; set; } = "Pending";

        [Required]
        [MaxLength(300)]
        public string shippingAddress { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
}
