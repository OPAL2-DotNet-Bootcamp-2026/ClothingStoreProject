using System.ComponentModel.DataAnnotations;

namespace ClothingStore.DTOs
{
    public class ReviewDTOs
    {

        //ReviewResponseDto

        public class ReviewResponseDto
        {
            public int ReviewId { get; set; }

            public int UserId { get; set; }

            public int ProductId { get; set; }

            public int Rating { get; set; }

            public string? Comment { get; set; }

            public DateTime ReviewDate { get; set; }
        }


        // CreateReviewDto

        public class CreateReviewDto
        {
            [Required(ErrorMessage = "Product Id is required.")]
            public int ProductId { get; set; }

            [Required(ErrorMessage = "Rating is required.")]
            [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
            public int Rating { get; set; }

            [StringLength(500, ErrorMessage = "Comment cannot exceed 500 characters.")]
            public string? Comment { get; set; }
        }



        // UpdateReviewDto

        public class UpdateReviewDto
        {
            [Required(ErrorMessage = "Rating is required.")]
            [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
            public int Rating { get; set; }

            [StringLength(500, ErrorMessage = "Comment cannot exceed 500 characters.")]
            public string? Comment { get; set; }
        }
    }


}




    











































