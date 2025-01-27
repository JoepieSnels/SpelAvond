using IndividueleCSharpProject.Domain;
using IndividueleCSharpProject.DomainServices.Repositories;
using IndividueleCSharpProject.DomainServices.ServicesIntr;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace IndividueleCSharpProject.DomainServices.ServicesInpl {
        public class SpelletjesavondService
        {
            private readonly IPersonsRepository _personsRepository;
            private readonly IReviewsRepository _reviewsRepository;
            private readonly IGameRepository _gamesRepository;
            private readonly IGameNightRepository _gameNightsRepository;

            public SpelletjesavondService(IPersonsRepository personsRepository, IReviewsRepository reviewsRepository, IGameRepository gamesRepository, IGameNightRepository gameNightsRepository)
            {
                _personsRepository = personsRepository;
                _reviewsRepository = reviewsRepository;
                _gamesRepository = gamesRepository;
                _gameNightsRepository = gameNightsRepository;
            }

            // GameNight CRUD operations
            public IEnumerable<GameNight> GetGameNights() => _gameNightsRepository.GetGameNights().ToList();
            public GameNight GetGameNight(int id) => _gameNightsRepository.GetGameNight(id);
            public void AddGameNight(GameNight gameNight) => _gameNightsRepository.AddGameNight(gameNight);
            public void UpdateGameNight(GameNight gameNight) => _gameNightsRepository.UpdateGameNight(gameNight);
            public void DeleteGameNight(int id) => _gameNightsRepository.DeleteGameNight(id);

            // Game CRUD operations
            public IEnumerable<Games> GetGames() => _gamesRepository.GetGames().ToList();
            public Games GetGame(int id) => _gamesRepository.GetGame(id);
            public void AddGame(Games game) => _gamesRepository.AddGame(game);
            public void UpdateGame(Games game) => _gamesRepository.UpdateGame(game);
            public void DeleteGame(int id) => _gamesRepository.DeleteGame(id);

            // Person CRUD operations
            public IEnumerable<Person> GetPersons() => _personsRepository.GetPersons().ToList();
            public Person GetPerson(int id) => _personsRepository.GetPerson(id);
            public void AddPerson(Person person) => _personsRepository.AddPerson(person);
            public void UpdatePerson(Person person) => _personsRepository.UpdatePerson(person);
            public void DeletePerson(int id) => _personsRepository.DeletePerson(id);

            // Review CRUD operations
            public IEnumerable<Review> GetReviews() => _reviewsRepository.GetReviews().ToList();
            public Review GetReview(int id) => _reviewsRepository.GetReview(id);
            public void AddReview(Review review) => _reviewsRepository.AddReview(review);
            public void UpdateReview(Review review) => _reviewsRepository.UpdateReview(review);
            public void DeleteReview(int id) => _reviewsRepository.DeleteReview(id);
        }
    }

