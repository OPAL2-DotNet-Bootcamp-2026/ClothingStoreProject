using ClothingStore.Models;

namespace ClothingStore.Repos
{
    public class ReviewRepo
    {

        private ClothingStoreContext context;

        public ReviewRepo(ClothingStoreContext context)
        {
            this.context = context;
        }


        public List<Review> GetAllReview()
        {
            return context.Reviews.ToList();

        }

        public Review? GetReviewById(int id)
        {
            return context.Reviews.FirstOrDefault(r => r.reviewId == id);
        }


        public void AddReview(Review review)
        {
            context.Reviews.Add(review);
            context.SaveChanges();
        }


        public List<Review> GetReviewsByRating(int rating)
        {
            return context.Reviews
                .Where(r => r.rating == rating)
                .ToList();
        }



        public List<Review> GetReviewsByUserId(int userId)
        {
            return context.Reviews
                .Where(r => r.userId == userId)
                .ToList();
        }



        public List<Review> GetReviewsByProductId(int productId)
        {
            return context.Reviews
                .Where(r => r.productId == productId)
                .ToList();
        }



        public List<Review> GetByMinRating(int rating)
        {
            return context.Reviews
                .Where(r => r.rating == rating)
                .ToList();
        }



        public Review? GetByUserAndProduct(int userId, int productId)
        {
            return context.Reviews.FirstOrDefault(r =>
                r.userId == userId &&
                r.productId == productId);
        }



        public void UpdateReview(Review review)
        {
            context.SaveChanges();
        }


        public void DeleteReview(int id)
        {
            var review = context.Reviews.FirstOrDefault(r => r.reviewId == id);

            if (review != null)
            {
                context.Reviews.Remove(review);
                context.SaveChanges();
            }













        }

      }
}
