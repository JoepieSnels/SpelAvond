using IndividueleCSharpProject.Domain;
using IndividueleCSharpProject.DomainServices.Repositories;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace IndividueleCSharpProject.Infrastructure
{
    public class GamesRepositoryEF : IGameRepository
    {
        private readonly GamenightDBContext _context;

        public GamesRepositoryEF(GamenightDBContext context)
        {
            _context = context;
        }

        public IQueryable<Games> GetGames()
        {
            return _context.GamesDb;
        }

        public Games GetGame(int id)
        {
            return _context.GamesDb.FirstOrDefault(g => g.gameId == id);
        }

        public void AddGame(Games game)
        {
            _context.GamesDb.Add(game);
            _context.SaveChanges();
        }

        public void UpdateGame(Games game)
        {
            _context.GamesDb.Update(game);
            _context.SaveChanges();
        }

        public void DeleteGame(int id)
        {
            Games game = _context.GamesDb.FirstOrDefault(g => g.gameId == id);
            _context.GamesDb.Remove(game);
            _context.SaveChanges();
        }
    }
}