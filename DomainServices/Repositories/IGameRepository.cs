namespace IndividueleCSharpProject.DomainServices.Repositories{
using System.Collections.Generic;
using IndividueleCSharpProject.Domain;

    public interface IGameRepository{
    public IQueryable<Games> GetGames();
    public Games GetGame(int id);
    public void AddGame(Games game);
    public void UpdateGame(Games game);
    public void DeleteGame(int id);
    
    

    }
}