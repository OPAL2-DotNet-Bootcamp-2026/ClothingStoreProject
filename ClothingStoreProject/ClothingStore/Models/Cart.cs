using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace ClothingStore.Models
    
{
    public class Cart
    {
        public int carId { get; set; }  
        public int userId {get; set;} 
        public DateTime createdAt {get; set;}
        public DateTime  updatedAt {get; set;}
        
    }
}
