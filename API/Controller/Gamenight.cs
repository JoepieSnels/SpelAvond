using Microsoft.AspNetCore.Mvc;
using IndividueleCSharpProject.Domain;
using IndividueleCSharpProject.DomainServices.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IndividueleCSharpProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameNightController : ControllerBase
    {
        private readonly IGameNightRepository _gameNightRepository;

        public GameNightController(IGameNightRepository gameNightRepository)
        {
            _gameNightRepository = gameNightRepository;
        }

        // GET: api/gamenight
        [HttpGet]
        public async Task<IActionResult> GetGameNights()
        {
            var gameNights = await _gameNightRepository.GetGameNights().ToListAsync();
            return Ok(gameNights);
        }

        // GET: api/gamenight/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGameNight(int id)
        {
            var gameNight = _gameNightRepository.GetGameNight(id);
            if (gameNight == null)
                return NotFound();

            return Ok(gameNight);
        }

        // POST: api/gamenight
        [HttpPost]
        public async Task<IActionResult> CreateGameNight([FromBody] GameNight gameNight)
        {
            if (gameNight == null)
                return BadRequest();

            _gameNightRepository.AddGameNight(gameNight);
            return CreatedAtAction(nameof(GetGameNight), new { id = gameNight.gameNightId }, gameNight);
        }

        // PUT: api/gamenight/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGameNight(int id, [FromBody] GameNight gameNight)
        {
            if (gameNight == null || gameNight.gameNightId != id)
                return BadRequest();

            var existingGameNight = _gameNightRepository.GetGameNight(id);
            if (existingGameNight == null)
                return NotFound();

            _gameNightRepository.UpdateGameNight(gameNight);
            return NoContent();  // Success with no content
        }

        // DELETE: api/gamenight/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameNight(int id)
        {
            var existingGameNight = _gameNightRepository.GetGameNight(id);
            if (existingGameNight == null)
                return NotFound();

            _gameNightRepository.DeleteGameNight(id);
            return NoContent();
        }

        // POST: api/gamenight/{gameNightId}/players/{personId}
        [HttpPost("{gameNightId}/players/{personId}")]
        public IActionResult AddPlayerToGameNight(int gameNightId, int personId)
        {
            var gameNight = _gameNightRepository.GetGameNight(gameNightId);
            if (gameNight == null)
                return NotFound();

            var isSignedUp = _gameNightRepository.isUserSignedUpForGameNight(personId, gameNightId);
            if (isSignedUp)
                return BadRequest("User is already signed up for this game night.");

            _gameNightRepository.AddGameNightPlayer(gameNightId, personId);
            return Ok(new { Message = "Player added to game night successfully!" });
        }
    }
}
