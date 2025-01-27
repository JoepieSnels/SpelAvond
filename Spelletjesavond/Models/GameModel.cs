using IndividueleCSharpProject.Domain;

namespace Spelletjesavond.Models
{
    public class GameModel
    {
        public int gameId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string imageUrl { get; set; }
        public Genrè genre { get; set; }
        public bool is18Plus { get; set; }
        public TypeOfGame typeOfGame { get; set; }


    }
}