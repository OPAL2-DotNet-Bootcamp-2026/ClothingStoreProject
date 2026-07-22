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
            return context.reviews.ToList();

        }

        public Review GetReviewById(int id)
        {
            return context.reviews.FirstOrDefault(r => r.reviewId == id);
        }


        public void AddReview(Review review)
        {
            context.reviews.Add(review);
            context.SaveChanges();
        }


        public List<Review> GetReviewsByRating(int rating)
        {
            return context.reviews
                .Where(r => r.rating == rating)
                .ToList();
        }



        public List<Review> GetReviewsByUserId(int userId)
        {
            return context.reviews
                .Where(r => r.userId == userId)
                .ToList();
        }



        public List<Review> GetReviewsByProductId(int productId)
        {
            return context.reviews
                .Where(r => r.productId == productId)
                .ToList();
        }


        public void UpdateReview(Review review)
        {
            context.SaveChanges();
        }


        public void DeleteReview(int id)
        {
            var review = context.reviews.FirstOrDefault(r => r.reviewId == id);

            if (review != null)
            {
                context.reviews.Remove(review);
                context.SaveChanges();
            }













        }

      }
}
