
namespace IndividueleCSharpProject.DomainServices.ServicesIntr

{
using System.Collections.Generic;
using IndividueleCSharpProject.Domain;
    public interface ISpelletjesavondService
    {
        public IEnumerable<GameNight> GetGameNights();
        public GameNight GetGameNight(int id);
        public void AddGameNight(GameNight gameNight);
        public void UpdateGameNight(GameNight gameNight);
        public void DeleteGameNight(int id);
        
        public IEnumerable<Games> GetGames();
        public Games GetGame(int id);
        public void AddGame(Games game);
        public void UpdateGame(Games game);
        public void DeleteGame(int id);

        public IEnumerable<Person> GetPersons();
        public Person GetPerson(int id);
        public void AddPerson(Person person);
        public void UpdatePerson(Person person);
        public void DeletePerson(int id);

        public IEnumerable<Review> GetReviews();
        public Review GetReview(int id);
        public void AddReview(Review review);
        public void UpdateReview(Review review);
        public void DeleteReview(int id);
    }
}