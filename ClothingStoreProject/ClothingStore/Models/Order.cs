namespace ClothingStore.Models
{
    public class Order
    {
        public int orderId {get; set;}
        public int userId {get; set;}
        public DateTime  orderDate {get; set;}
        public decimal totalPrice {get; set;}
        public string status = "pendeng" {get; set;}
        public string shippingAddress {get; set;}
        
    }
}
