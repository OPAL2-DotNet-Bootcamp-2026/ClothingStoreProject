using ClothingStore.Services;
using Microsoft.AspNetCore.Mvc;
using static ClothingStore.DTOs.ReviewDTOs;

namespace ClothingStore.Controllers
{
    public class ReviewController : ControllerBase
    {

        private ReviewService reviewService;

        public ReviewController(ReviewService reviewService)
        {

            this.reviewService = reviewService;
        }



        // GET: review/GetAllReviews
        [HttpGet("GetAllReviews")]
        public IActionResult GetAllReviews()
        {
            return Ok(reviewService.GetAllReviews());
        }




        // GET: review/GetReviewById?id=1
        [HttpGet("GetReviewById")]
        public IActionResult GetReviewById([FromQuery] int id)
        {
            var review = reviewService.GetReviewById(id);

            if (review == null)
            {
                return NotFound("Review not found.");
            }

            return Ok(review);
        }




        // GET: review/GetReviewsByProductId?productId=1
        [HttpGet("GetReviewsByProductId")]
        public IActionResult GetReviewsByProductId([FromQuery] int productId)
        {
            return Ok(reviewService.GetReviewsByProductId(productId));
        }




        // GET: review/GetReviewsByUserId?userId=1
        [HttpGet("GetReviewsByUserId")]
        public IActionResult GetReviewsByUserId([FromQuery] int userId)
        {
            return Ok(reviewService.GetReviewsByUserId(userId));
        }



        // GET: review/GetReviewsByRating?rating=5
        [HttpGet("GetReviewsByRating")]
        public IActionResult GetReviewsByRating([FromQuery] int rating)
        {
            return Ok(reviewService.GetReviewsByRating(rating));
        }



        // GET: review/GetReviewsByMinRating?rating=4
        [HttpGet("GetReviewsByMinRating")]
        public IActionResult GetReviewsByMinRating([FromQuery] int rating)
        {
            return Ok(reviewService.GetReviewsByMinRating(rating));
        }



        // POST: review/AddReview
        [HttpPost("AddReview")]
        public IActionResult AddReview([FromQuery] int userId, [FromBody] CreateReviewDto dto)
        {

            var review = reviewService.AddReview(userId, dto);

            if (review == null)
            {
                return BadRequest("User already reviewed this product.");
            }

            return Ok(review);
        }




        // PUT: review/UpdateReview?id=1
        [HttpPut("UpdateReview")]
        public IActionResult UpdateReview([FromQuery] int id,[FromBody] UpdateReviewDto dto)
        {
            bool updated = reviewService.UpdateReview(id, dto);

            if (!updated)
            {
                return NotFound("Review not found.");
            }

            return Ok("Review updated successfully.");
        }





        // DELETE: review/DeleteReview?id=1
        [HttpDelete("DeleteReview")]
        public IActionResult DeleteReview([FromQuery] int id)
        {
            bool deleted = reviewService.DeleteReview(id);

            if (!deleted)
            {
                return NotFound("Review not found.");
            }

            return Ok("Review deleted successfully.");
        }





    }

        }

