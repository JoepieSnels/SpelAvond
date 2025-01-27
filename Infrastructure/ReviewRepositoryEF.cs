using IndividueleCSharpProject.Domain;
using IndividueleCSharpProject.DomainServices.Repositories;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace IndividueleCSharpProject.Infrastructure
{
    public class ReviewRepositoryEF : IReviewsRepository
    {
        private readonly GamenightDBContext _context;
        public ReviewRepositoryEF(GamenightDBContext context)
        {
            _context = context;
        }
        public IEnumerable<Review> GetReviews()
        {
            return _context.Reviews.ToList();
        }
        public Review GetReview(int id)
        {
            return _context.Reviews.FirstOrDefault(r => r.reviewId == id);
        }
        public void AddReview(Review review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
        }
        public void UpdateReview(Review review)
        {
            _context.Reviews.Update(review);
            _context.SaveChanges();
        }
        public void DeleteReview(int id)
        {
            Review review = _context.Reviews.FirstOrDefault(r => r.reviewId == id);
            _context.Reviews.Remove(review);
            _context.SaveChanges();
        }
    }
}