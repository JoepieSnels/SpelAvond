using System.ComponentModel.DataAnnotations;
using IndividueleCSharpProject.Domain;

namespace Spelletjesavond.Models
{
    public class GameNightModel
    {
        public int gameNightId { get; set; }
        [Required(ErrorMessage = "De datum is verplicht.")]
        [CustomValidation(typeof(GameNightModel), nameof(ValidateDate))]
        public DateTime dateTime { get; set; }
        
        [Required(ErrorMessage = "De locatie is verplicht.")]
        public string address { get; set; }

        [Range(1, 100, ErrorMessage = "Het maximaal aantal spelers moet tussen 1 en 100 liggen.")]
        public int maxPlayers { get; set; }

        public bool is18Plus { get; set; }
        public bool lactoseFree { get; set; }
        public bool nutFree { get; set; }
        public bool vegetarian { get; set; }
        public bool alcoholic { get; set; }

        [Required(ErrorMessage = "Selecteer minimaal één spel.")]
        public List<int> games { get; set; } = new();
        public List<String> food { get; set; } = new();
          public static ValidationResult? ValidateDate(DateTime date, ValidationContext context)
        {
            if (date <= DateTime.Now)
            {
                return new ValidationResult("De datum moet in de toekomst liggen.");
            }
            return ValidationResult.Success;
        }
    }

    public class GameNightDetailsModel
    {
        internal bool isAlreadySignedUp;

        public int gameNightId { get; set; }
        public Person host { get; set; }
        public int hostId { get; set; }
        public string address { get; set; }
        public DateTime dateTime { get; set; }
        public int maxPlayers { get; set; }
        public List<Person> players { get; set; } = new List<Person>();
        public ICollection<Review> reviews { get; set; } = new List<Review>();
        public ICollection<Games> games { get; set; } = new List<Games>();
        public string food { get; set; }
        public bool lactoseFree { get; set; }
        public bool alcoholic { get; set; }
        public bool nutFree { get; set; }
        public bool vegetarian { get; set; }
        public bool is18Plus { get; set; }
        public double averageScore { get; set; }
        public bool hasReviewed { get; set; }
        

    }
    public class GameNightOverviewModel
{
    public int gameNightId { get; set; }
    public DateTime dateTime { get; set; }
    public string address { get; set; }
    public int maxPlayers { get; set; }
    public int currentPlayersCount { get; set; }
    public bool is18Plus { get; set; }
}

}
