using ClothingStore.Models;
using ClothingStore.Repos;
using static ClothingStore.DTOs.ReviewDTOs;



namespace ClothingStore.Services
{
    public class ReviewService
    {
        private readonly ReviewRepo repo;

        public ReviewService(ReviewRepo _repo)

        {
            repo = repo;
        }



        private ReviewResponseDto MapToResponse(Review review)
        {
            return new ReviewResponseDto
            {
                ReviewId = review.reviewId,
                UserId = review.userId,
                ProductId = review.productId,
                Rating = review.rating,
                Comment = review.comment,
                ReviewDate = review.reviewDate
            };
        }



        public List<ReviewResponseDto> GetAllReviews()
        {
            return repo.GetAllReview()
                .Select(MapToResponse)
                .ToList();
        }



        public ReviewResponseDto? GetReviewById(int id)
        {
            Review? review = repo.GetReviewById(id);

            if (review == null)
                return null;

            return MapToResponse(review);
        }



        public List<ReviewResponseDto> GetReviewsByProductId(int productId)
        {
            return repo.GetReviewsByProductId(productId)
                .Select(MapToResponse)
                .ToList();
        }



        public List<ReviewResponseDto> GetReviewsByUserId(int userId)
        {
            return repo.GetReviewsByUserId(userId)
                .Select(MapToResponse)
                .ToList();
        }



        public List<ReviewResponseDto> GetReviewsByRating(int rating)
        {
            return repo.GetReviewsByRating(rating)
                .Select(MapToResponse)
                .ToList();
        }



        public List<ReviewResponseDto> GetReviewsByMinRating(int rating)
        {
            return repo.GetByMinRating(rating)
                .Select(MapToResponse)
                .ToList();
        }



        public ReviewResponseDto? AddReview(int userId, CreateReviewDto dto)
        {
            Review? exists = repo.(userId, dto.ProductId);

            if (exists != null)
                return null;

            Review review = new Review
            {
                userId = userId,
                productId = dto.ProductId,
                rating = dto.Rating,
                comment = dto.Comment,
                reviewDate = DateTime.Now
            };

            repo.AddReview(review);

            return MapToResponse(review);
        }



        public bool UpdateReview(int id, UpdateReviewDto dto)
        {
            Review? review = repo.GetReviewById(id);

            if (review == null)
                return false;

            review.rating = dto.Rating;
            review.comment = dto.Comment;

            repo.UpdateReview(review);

            return true;
        }





        public bool DeleteReview(int id)
        {
            Review? review = repo.GetReviewById(id);

            if (review == null)
                return false;

            repo.DeleteReview(id);

            return true;
        }




        public double GetAverageRating(int productId)
        {
            List<Review> reviews = repo.GetReviewsByProductId(productId);

            if (reviews.Count == 0)
                return 0;

            return reviews.Average(r => r.rating);
        }
    }
}















    }






















}

    

