namespace IndividueleCSharpProject.DomainServices.Repositories
{
    using System.Collections.Generic;
    using IndividueleCSharpProject.Domain;
    public interface IReviewsRepository
    {
        public IEnumerable<Review> GetReviews();
        public Review GetReview(int id);
        public void AddReview(Review review);
        public void UpdateReview(Review review);
        public void DeleteReview(int id);
    }
}
