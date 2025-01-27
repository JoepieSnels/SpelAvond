using System.ComponentModel.DataAnnotations.Schema;

namespace IndividueleCSharpProject.Domain
{
    public class GameNight
    {
        public int gameNightId { get; set; }
        [ForeignKey("personId")]
        public int hostId { get; set; }
        public string address { get; set; }
        public DateTime dateTime { get; set; }
        public int maxPlayers { get; set; }
        public ICollection<Person>? players { get; set; } = new List<Person>();
        public ICollection<Review>? reviews { get; set; } = new List<Review>();
        public ICollection<Games> games { get; set; } = new List<Games>();
        public string food { get; set; } = "No food";
        public bool lactoseFree { get; set; } = false;
        public bool alcoholic { get; set; } = false;
        public bool nutFree { get; set; } = false;
        public bool vegetarian { get; set; } = false;
        public bool is18Plus { get; set; } = false;
        public GameNight() { }



        public GameNight(int host, string address, DateTime dateTime, bool lactoseFree, bool alcoholic, bool nutFree, bool vegetarian, int maxPlayers, bool is18Plus, ICollection<Games> games, ICollection<Person> players)
         
        {
            hostId = host;
            this.address = address;
            this.dateTime = dateTime;
            games = games;
            players = players;
            reviews = new List<Review>();
            food = "No food";
            this.lactoseFree = lactoseFree;
            this.alcoholic = alcoholic;
            this.nutFree = nutFree;
            this.vegetarian = vegetarian;
            this.maxPlayers = maxPlayers;
            this.is18Plus = is18Plus;
        }
        public GameNight(int gameNightId, int host, string address, DateTime dateTime, bool lactoseFree, bool alcoholic, bool nutFree, bool vegetarian, int maxPlayers, bool is18Plus, ICollection<Games> games, ICollection<Person> players)
            : this(host, address, dateTime, lactoseFree, alcoholic, nutFree, vegetarian, maxPlayers, is18Plus, games, players)
        {
            this.gameNightId = gameNightId; 
        }
        
    
    }
    
}