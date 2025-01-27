using IndividueleCSharpProject.Domain;
using IndividueleCSharpProject.DomainServices.Repositories;
using Moq;


namespace IndividueleCSharpProject.Tests
{
    public class ReviewsRepositoryTests
    {
   
        private Mock<IReviewsRepository> _mockReviewsRepository;

        public ReviewsRepositoryTests()
        {
            _mockReviewsRepository = new Mock<IReviewsRepository>();
        }

        [Fact]
        public void AddReview_ShouldAddReview()
        {
            // Arrange
            var review = new Review(
                reviewId: 1,
                rating: 4,
                comment: "Great game!",
                gameNight: 1,
                person: 1
            );

            // Act
            _mockReviewsRepository.Setup(x => x.AddReview(review));
            _mockReviewsRepository.Object.AddReview(review);

            // Assert
            _mockReviewsRepository.Verify(x => x.AddReview(review), Times.Once);

        }

        [Fact]
        public void GetReview_ShouldReturnReview()
        {
            // Arrange
            var review = new Review(
                reviewId: 1,
                rating: 4,
                comment: "Great game!",
                gameNight: 1,
                person: 1
            );

            // Act
            _mockReviewsRepository.Setup(x => x.GetReview(1)).Returns(review);
            var result = _mockReviewsRepository.Object.GetReview(1);

            // Assert
            Assert.Equal(review, result);
        }

        [Fact]
        public void DeleteReview_ShouldDeleteReview()
        {
            // Arrange
            var reviewId = 1;

            // Act
            _mockReviewsRepository.Setup(x => x.DeleteReview(reviewId));
            _mockReviewsRepository.Object.DeleteReview(reviewId);

            // Assert
            _mockReviewsRepository.Verify(x => x.DeleteReview(reviewId), Times.Once);
        }

        [Fact]
        public void UpdateReview_ShouldUpdateReview()
        {
            // Arrange
            var review = new Review(
                reviewId: 1,
                rating: 4,
                comment: "Great game!",
                gameNight: 1,
                person: 1
            );

            // Act
            _mockReviewsRepository.Setup(x => x.UpdateReview(review));
            _mockReviewsRepository.Object.UpdateReview(review);

            // Assert
            _mockReviewsRepository.Verify(x => x.UpdateReview(review), Times.Once);
        }

        [Fact]
        public void GetReviews_ShouldReturnReviews()
        {
            // Arrange
            var review = new Review(
                reviewId: 1,
                rating: 4,
                comment: "Great game!",
                gameNight: 1,
                person: 1
            );

            // Act
            _mockReviewsRepository.Setup(x => x.GetReviews()).Returns(new List<Review> { review });
            var result = _mockReviewsRepository.Object.GetReviews();

            // Assert
            Assert.Single(result);
            Assert.Contains(review, result);
        }
    }
}
        