using System.ComponentModel.DataAnnotations.Schema;

namespace IndividueleCSharpProject.Domain
{
    public class Review
    {
        public int reviewId { get; set; }
        private int _rating;

        public int rating
        {
            get { return _rating; }
            set
            {
                if (value > 5 || value < 1)
                {
                 throw new ArgumentException("Rating must be between 1 and 5");
                }
                _rating = value; // Store the value in the backing field
            }
        }

        public string comment { get; set; }
        public Person reviewer { get; set; }
        [ForeignKey("personId")]
        public int reviewerId { get; init; }
        
        
        public GameNight gameNight { get; set; }
        [ForeignKey("gameNightId")]
        public int gameNightId { get; init; }

         public Review() { }

        public Review(int rating, string comment, int person, int gameNight)
        {
            this.rating = rating;
            this.comment = comment;
            this.reviewerId = person;
            this.gameNightId = gameNight;
           
        }
        public Review(int reviewId, int rating, string comment, int person, int gameNight) : this(rating, comment, person, gameNight) => this.reviewId = reviewId;
    }
}