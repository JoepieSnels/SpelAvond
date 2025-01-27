using IndividueleCSharpProject.Domain;
using IndividueleCSharpProject.DomainServices.Repositories;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace IndividueleCSharpProject.Infrastructure
{
    public class GamenightRepositoryEF : IGameNightRepository
    {
        private readonly GamenightDBContext _context;
        public GamenightRepositoryEF(GamenightDBContext context)
        {
            _context = context;
        }
        public IQueryable<GameNight> GetGameNights()
        {
            return _context.GameNights.Include(g => g.games);
        }
        public GameNight GetGameNight(int id)
        {
            return _context.GameNights.Include(g => g.games).Include(g => g.players).FirstOrDefault(g => g.gameNightId == id);
        }
        public void AddGameNight(GameNight gameNight)
        {
            _context.GameNights.Add(gameNight);
            _context.SaveChanges();
        }
        public void UpdateGameNight(GameNight gameNight)
        {
            _context.GameNights.Update(gameNight);
            _context.SaveChanges();
        }
        public void DeleteGameNight(int id)
        {
            GameNight gameNight = _context.GameNights.FirstOrDefault(g => g.gameNightId == id);
            _context.GameNights.Remove(gameNight);
            _context.SaveChanges();
        }
        public void AddGameNightPlayer(int gameNightId, int personId)
        {
            GameNight gameNight = _context.GameNights.Include(g => g.players).FirstOrDefault(g => g.gameNightId == gameNightId);
            Person person = _context.Persons.FirstOrDefault(p => p.personId == personId);
            gameNight.players.Add(person);
            _context.SaveChanges();
        }
        public bool isUserSignedUpForGameNight(int personId, int gameNightId)
        {
            GameNight gameNight = _context.GameNights.Include(g => g.players).FirstOrDefault(g => g.gameNightId == gameNightId);
            return gameNight.players.Any(p => p.personId == personId);
        }
       
    }
}