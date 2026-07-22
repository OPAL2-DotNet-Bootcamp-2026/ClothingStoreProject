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

        public void UpdateReview(Review review)
        {
            context.reviews.Update(review);
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
