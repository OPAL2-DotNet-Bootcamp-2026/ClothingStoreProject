using ClothingStore.Models;
using ClothingStore.Repos;

namespace ClothingStore.Services
{
    public class ReviewService
    {
        private ReviewRepo repo;

        public ReviewService(ReviewRepo repo)

        {
            this.repo=repo;
        }


        public List<Review> GetAllReview()
        {
            return repo.GetAllReview();
        }

        public Review GetReviewById(int id)
        {
            return repo.GetReviewById(id);
        }

        public void AddReview(Review review)
        {
            repo.AddReview(review);
        }

        public void UpdateReview(Review review)
        {
            repo.UpdateReview(review);
        }

        public void DeleteReview(int id)
        {
            repo.DeleteReview(id);
        }
    }
}

















    }
}
