namespace IndividueleCSharpProject.DomainServices.Repositories
{
    using System.Collections.Generic;
    using IndividueleCSharpProject.Domain;

    public interface IGameNightRepository
    {
        public IQueryable<GameNight> GetGameNights();
        public GameNight GetGameNight(int id);
        public void AddGameNight(GameNight gameNight);
        public void UpdateGameNight(GameNight gameNight);
        public void DeleteGameNight(int id);
        public void AddGameNightPlayer(int gameNightId, int personId);
        public bool isUserSignedUpForGameNight(int personId, int gameNightId);
    }
}
    