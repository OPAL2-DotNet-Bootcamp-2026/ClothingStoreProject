using ClothingStore.Models;
using ClothingStore.Repos;

namespace ClothingStore.Services
{
    public class ReviewService
    {
        private readonly ReviewRepo repo;

        public ReviewService(ReviewRepo repo)

        {
            this.repo = repo;
        }

        // Get All Reviews
        public List<Review> GetAllReview()
        {
            return repo.GetAllReview();
        }



        // Get Review By Id
        public Review GetReviewById(int id)
        {
            if (id <= 0)
            {
                return null;
            }

            return repo.GetReviewById(id);
        }



        // Add Review
        public void AddReview(Review review)
        {
            if (review == null)
            {
                return;
            }


            if (string.IsNullOrWhiteSpace(review.comment))
            {
                return;
            }

            if (review.rating < 1 || review.rating > 5)
            {
                return;
            }


            repo.AddReview(review);
        }



        // Get Reviews By Rating
        public List<Review> GetReviewsByRating(int rating)
        {
            if (rating < 1 || rating > 5)
            {
                return new List<Review>();
            }

            return repo.GetReviewsByRating(rating);
        }



        // Get Reviews By User Id
        public List<Review> GetReviewsByUserId(int userId)
        {
            if (userId <= 0)
            {
                return new List<Review>();
            }

            return repo.GetReviewsByUserId(userId);
        }




        // Get Reviews By Product Id
        public List<Review> GetReviewsByProductId(int productId)
        {
            if (productId <= 0)
            {
                return new List<Review>();
            }

            return repo.GetReviewsByProductId(productId);
        }




        // Update Review
        public void UpdateReview(Review review)
        {

            if (review == null)
            {
                return;
            }

            if (review.reviewId <= 0)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(review.comment))
            {
                return;
            }

            if (review.rating < 1 || review.rating > 5)
            {
                return;
            }


            repo.UpdateReview(review);
        }





        // Delete Review
        public void DeleteReview(int id)
        {

            if (id <= 0)
            {
                return;
            }


            repo.DeleteReview(id);
        }
    }

    }

