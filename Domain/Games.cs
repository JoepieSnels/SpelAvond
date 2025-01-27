namespace IndividueleCSharpProject.Domain
{
    public class Games()
    {
        public int gameId { get; set; }
        public string name { get; init; }
        public string description { get; init; }
        public Genrè genre { get; init; }
        public bool is18Plus { get; init; }
        public string image { get; set; }
        public TypeOfGame typeOfGame { get; init; }
        public ICollection<GameNight> gameNights { get; set; } = new List<GameNight>();


        public Games(string name, string description, Genrè genre, bool is18Plus, string image, TypeOfGame typeOfGame) : this()
        {
            this.name = name;
            this.description = description;
            this.genre = genre;
            this.is18Plus = is18Plus;
            this.image = image;
            this.typeOfGame = typeOfGame;
        }
        public Games(int gameId, string name, string description, Genrè genre, bool is18Plus, string image, TypeOfGame typeOfGame) : this(name, description, genre, is18Plus, image, typeOfGame) => this.gameId = gameId;
        
    }
}