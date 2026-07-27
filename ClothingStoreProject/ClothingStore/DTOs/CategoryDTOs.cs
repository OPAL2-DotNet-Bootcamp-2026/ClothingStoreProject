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
    }
}
