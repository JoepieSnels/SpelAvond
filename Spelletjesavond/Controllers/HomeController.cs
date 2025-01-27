using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Spelletjesavond.Models;
using IndividueleCSharpProject.DomainServices.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using IndividueleCSharpProject.Domain;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Spelletjesavond.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IGameNightRepository _gameNightRepository;
    private readonly IPersonsRepository _personsRepository;
    private readonly IGameRepository _gameRepository;
    private readonly IReviewsRepository _reviewRepository;
    private readonly UserManager<IdentityUser> _userManager;

    public HomeController(ILogger<HomeController> logger, IGameNightRepository gameNightRepository, IPersonsRepository personsRepository, IGameRepository gameRepository, IReviewsRepository reviewRepository, UserManager<IdentityUser> userManager)
    {
        _logger = logger;
        _gameNightRepository = gameNightRepository;
        _personsRepository = personsRepository;
        _gameRepository = gameRepository;
        _reviewRepository = reviewRepository;
        _userManager = userManager;

    }

    [Authorize]
    public IActionResult Index()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login", "Account");
        }

        // Haal de ingelogde gebruiker op via hun email
        var email = User.Identity.Name; // Dit haalt het emailadres van de ingelogde gebruiker op

        // Haal de persoon op uit de Persons tabel op basis van het emailadres
        var person = _personsRepository.GetPersons().FirstOrDefault(p => p.email == email);

        if (person == null)
        {
            // Handle als de persoon niet gevonden is (bijvoorbeeld: gebruiker is niet geregistreerd)
            return View(new List<GameNightDetailsModel>());
        }



        // Haal de game nights op waarvoor deze gebruiker zich heeft ingeschreven
        var gameNights = _gameNightRepository.GetGameNights()
                                             .Include(g => g.games)
                                             .Where(g => g.players.Any(gnu => gnu.personId == person.personId)) // Filter op ingelogde gebruiker
                                             .ToList();

        // Als er geen game nights zijn voor de gebruiker, geef een lege lijst weer
        if (!gameNights.Any())
        {
            return View(new List<GameNightDetailsModel>());
        }



        // Zet de game nights om naar een model voor de view
        var gameNightModels = gameNights.Select(g =>
        {
            // Haal de host op voor deze specifieke game night
            var host = _personsRepository.GetPersons().FirstOrDefault(p => p.personId == g.hostId);

            if (host == null)
            {
                return null; // Als er geen host is, return null (je kunt hier eventueel nog foutafhandeling doen)
            }

            return new GameNightDetailsModel
            {
                gameNightId = g.gameNightId,
                dateTime = g.dateTime,
                address = g.address,
                host = new Person(
                    host.personId,
                    host.firstName,
                    host.lastName
                ),
                games = g.games.ToList()
            };
        }).Where(model => model != null).ToList(); // Verwijder null waarden als de host niet gevonden is

        return View(gameNightModels);
    }
    public IActionResult Privacy()
    {
        return View();
    }
    [Authorize]
    public IActionResult MyGameNights()
    {
        var email = User.Identity.Name; // Haal de e-mail van de ingelogde gebruiker
        _logger.LogInformation("Ingelogde gebruiker email: {email}", email);

        var person = _personsRepository.GetPersons().FirstOrDefault(p => p.email == email);
        if (person == null)
        {
            _logger.LogWarning("Geen persoon gevonden voor email: {email}", email);
            return View(new List<GameNightDetailsModel>()); // Zorg dat de view een lege lijst van het juiste type krijgt
        }

        var gameNights = _gameNightRepository.GetGameNights()
            .Include(g => g.games)
            .Include(g => g.players)
            .Where(g => g.hostId == person.personId)
            .ToList();

        if (!gameNights.Any())
        {
            _logger.LogInformation("Geen game nights gevonden voor host met personId: {personId}", person.personId);
            return View(new List<GameNightDetailsModel>()); // Zorg dat de view een lege lijst van het juiste type krijgt
        }

        var gameNightModels = gameNights.Select(g => new GameNightDetailsModel
        {
            gameNightId = g.gameNightId,
            dateTime = g.dateTime,
            address = g.address,
            players = g.players?.Select(player => new Person(
                player.personId,
                player.firstName,
                player.lastName
            )).ToList() ?? new List<Person>(),
            maxPlayers = g.maxPlayers,
            games = g.games.Select(game => new Games
            {
                gameId = game.gameId,
                name = game.name
            }).ToList()
        }).ToList();

        _logger.LogInformation("Aantal game nights gevonden: {count}", gameNightModels.Count);
        return View(gameNightModels); // Geef de juiste lijst door
    }


    [Authorize]
    [HttpGet]
    public IActionResult CreateGameNight()
    {
        // Haal het e-mailadres van de ingelogde gebruiker op
        var userEmail = User.FindFirstValue(ClaimTypes.Email);

        // Haal de gebruiker op uit de database
        var person = _personsRepository.GetPersons().FirstOrDefault(p => p.email == userEmail);
        if (person == null)
        {
            // Gebruiker is niet gevonden; redirect naar foutpagina of login
            return Unauthorized();
        }

        // Controleer of de gebruiker 18 jaar of ouder is
        var userAge = DateTime.Now.Year - person.birthDate.Year;
        if (person.birthDate.Date > DateTime.Now.AddYears(-userAge))
        {
            userAge--; // Corrigeer de leeftijd als de verjaardag nog niet geweest is dit jaar
        }

        if (userAge < 18)
        {
            // Als de gebruiker jonger is dan 18, toon een foutmelding of redirect
            TempData["ErrorMessage"] = "Je moet 18 jaar of ouder zijn om een spellenavond aan te maken.";
            return RedirectToAction("Index", "Home");
        }

        // Haal alle beschikbare spellen op en stel ze in de ViewBag
        var games = _gameRepository.GetGames();
        ViewBag.Games = new MultiSelectList(games, "gameId", "name");

        // Initialiseer een nieuw GameNightModel
        var model = new GameNightModel();

        return View(model);
    }


    [HttpPost]
    public IActionResult CreateGameNight(GameNightModel model)
    {
        if (!ModelState.IsValid)
        {
            var games = _gameRepository.GetGames();
            ViewBag.Games = new MultiSelectList(games, "gameId", "name");
            return View(model);
        }

        var email = User.Identity.Name;
        var host = _personsRepository.GetPersons().FirstOrDefault(p => p.email == email);
        if (host == null)
        {
            ModelState.AddModelError("", "Ingelogde gebruiker kon niet worden gevonden.");
            var games = _gameRepository.GetGames();
            ViewBag.Games = new MultiSelectList(games, "gameId", "name");
            return View(model);
        }

        if (model.games == null || !model.games.Any())
        {
            ModelState.AddModelError("", "Selecteer minimaal één spel.");
            var games = _gameRepository.GetGames();
            ViewBag.Games = new MultiSelectList(games, "gameId", "name");
            return View(model);
        }

        // Haal de geselecteerde spellen op uit de database
        var selectedGames = _gameRepository.GetGames()
            .Where(g => model.games.Contains(g.gameId))
            .ToList();

        // Controleer of een van de geselecteerde spellen 18+ is
        var hasAdultGame = selectedGames.Any(g => g.is18Plus);

        // Voeg de game night toe met de aangepaste is18Plus-logica
        var gameNight = new GameNight
        {
            dateTime = model.dateTime,
            address = model.address,
            maxPlayers = model.maxPlayers,
            hostId = host.personId,
            is18Plus = hasAdultGame || model.is18Plus, // Automatisch 18+ als er een 18+ spel is
            lactoseFree = model.lactoseFree,
            nutFree = model.nutFree,
            vegetarian = model.vegetarian,
            alcoholic = model.alcoholic,
            players = new List<Person> { },
            games = selectedGames,
            food = string.Join(", ", model.food) // Converteer de lijst naar een string
        };

        // Voeg de GameNight toe aan de database
        _gameNightRepository.AddGameNight(gameNight);

        return RedirectToAction("Index");
    }

    [Authorize]
    public IActionResult GameInfo(int id)
    {
        var game = _gameRepository.GetGame(id);  // Haal de game op via het ID
        _logger.LogInformation("Game: {game}", game.name);
        if (game == null)
        {
            return NotFound();  // Als de game niet bestaat, geef een 404-pagina
        }
        return View(game);  // Geef de specifieke game door naar de View
    }
    [Authorize]
    public IActionResult GameNightInfo(int id)
    {
        // Haal de GameNight op via het ID
        var gameNight = _gameNightRepository.GetGameNight(id);
        var host = _personsRepository.GetPersons().FirstOrDefault(p => p.personId == gameNight.hostId);

        if (gameNight == null)
        {
            return NotFound(); // Als de GameNight niet bestaat, toon een 404-pagina
        }

        // Verkrijg het emailadres van de ingelogde gebruiker vanuit de claims (je kan ook andere claims gebruiken)
        var userEmail = User.FindFirstValue(ClaimTypes.Email);

        // Haal de persoon op uit de Persons tabel op basis van het emailadres
        var person = _personsRepository.GetPersons().FirstOrDefault(p => p.email == userEmail);

        if (person == null)
        {
            return Unauthorized(); // Als de gebruiker niet gevonden wordt, stuur ze naar een foutpagina
        }
        var averageScore = _gameNightRepository.GetGameNights()
        .Where(g => g.gameNightId == gameNight.gameNightId)
        .Include(g => g.reviews)
        .Select(g => g.reviews.Any() ? g.reviews.Average(r => r.rating) : 0) // Als er geen reviews zijn, geef 0 terug
        .FirstOrDefault();


        // Verkrijg het personId van de ingelogde gebruiker
        var personId = person.personId;

        // Controleer of de gebruiker al is ingeschreven voor deze GameNight
        var isAlreadySignedUp = _gameNightRepository.isUserSignedUpForGameNight(personId, id);

        bool hasReviewed = _reviewRepository.GetReviews()
        .Any(r => r.gameNightId == gameNight.gameNightId && r.reviewerId == personId);


        // Vul het model
        var model = new GameNightDetailsModel
        {
            host = host!,
            address = gameNight.address,
            dateTime = gameNight.dateTime,
            maxPlayers = gameNight.maxPlayers,
            players = gameNight.players?.ToList() ?? new List<Person>(),
            reviews = gameNight.reviews?.ToList() ?? new List<Review>(),
            games = gameNight.games,
            food = gameNight.food,
            lactoseFree = gameNight.lactoseFree,
            alcoholic = gameNight.alcoholic,
            nutFree = gameNight.nutFree,
            vegetarian = gameNight.vegetarian,
            is18Plus = gameNight.is18Plus,
            gameNightId = gameNight.gameNightId,
            hasReviewed = hasReviewed,
            isAlreadySignedUp = isAlreadySignedUp,
            averageScore = averageScore // Voeg de status van 'hasReviewed' toe
        };

        return View(model); // Retourneer de view met het model
    }



    [HttpPost]
    public IActionResult SignUpForGameNight(int gameNightId)
    {
        // Verkrijg het emailadres van de ingelogde gebruiker vanuit de claims
        var userEmail = User.FindFirstValue(ClaimTypes.Email);

        // Haal de persoon op uit de Persons tabel op basis van het emailadres
        var person = _personsRepository.GetPersons().FirstOrDefault(p => p.email == userEmail);

        if (person == null)
        {
            return Unauthorized(); // Als de gebruiker niet gevonden wordt, stuur ze naar een foutpagina
        }

        // Haal de spelavond op
        var gameNight = _gameNightRepository.GetGameNights().Include(g => g.players).FirstOrDefault(g => g.gameNightId == gameNightId);

        if (gameNight == null)
        {
            TempData["Error"] = "De gekozen spelavond bestaat niet.";
            return RedirectToAction("Index", "Home");
        }
        // Controleer of de spelavond al vol is;
        if (gameNight.players.Count == gameNight.maxPlayers)
        {
            TempData["Error"] = "Deze spelavond is al vol.";
            return RedirectToAction("AvailableGameNights", "Home");
        }
        var currentDate = DateTime.Now;
        var age = currentDate.Year - person.birthDate.Year;

        if (person.birthDate > currentDate.AddYears(-age))
        {
            age--;
        }

        // Controleer of de spelavond 18+ is en of de speler jonger is dan 18
        if (gameNight.is18Plus && age < 18)
        {
            TempData["Error"] = "Deze spelavond is alleen voor spelers van 18 jaar en ouder.";
            return RedirectToAction("Index", "Home");
        }

        // Controleer of de speler zich al heeft aangemeld voor een andere spelavond op dezelfde dag
        var gameNightDate = gameNight.dateTime.Date; // Datum van de huidige spelavond
        var existingSignUp = _gameNightRepository.GetGameNights()
            .Any(g => g.dateTime.Date == gameNightDate && g.players.Any(p => p.personId == person.personId));

        if (existingSignUp)
        {
            TempData["Error"] = "Je bent al aangemeld voor een andere spelavond op deze dag.";
            return RedirectToAction("Index", "Home");
        }

        // Voeg de inschrijving toe aan de GameNightPlayers tabel
        _gameNightRepository.AddGameNightPlayer(gameNightId, person.personId);

        // Bevestigingsmelding en redirect
        TempData["Success"] = "Je bent succesvol aangemeld voor de spelavond!";
        return RedirectToAction("Index", "Home");
    }






    [Authorize]

    public IActionResult AvailableGameNights()
    {
        // Haal de huidige gebruiker op
        var email = User.Identity.Name;
        var currentUser = _personsRepository.GetPersons().FirstOrDefault(p => p.email == email);

        if (currentUser == null)
        {
            return Unauthorized();
        }

        // Bereken de leeftijd van de gebruiker
        var currentDate = DateTime.Now;
        var age = currentDate.Year - currentUser.birthDate.Year;

        if (currentUser.birthDate > currentDate.AddYears(-age))
        {
            age--;
        }

        // Haal de beschikbare GameNights op
        var availableGameNights = _gameNightRepository.GetGameNights()
            .Include(gn => gn.players)
            .Where(gn =>
                gn.dateTime > DateTime.Now && // Alleen toekomstige GameNights
                (!gn.is18Plus || age >= 18) && // Filter 18+ GameNights als de gebruiker jonger is dan 18
                !gn.players.Any(p => p.personId == currentUser.personId) // Zorg dat de gebruiker nog niet is ingeschreven
            )
            .ToList();

        // Zet de lijst om naar een model voor de view
        var model = availableGameNights.Select(gn => new GameNightOverviewModel
        {
            gameNightId = gn.gameNightId,
            dateTime = gn.dateTime,
            address = gn.address,
            maxPlayers = gn.maxPlayers,
            currentPlayersCount = gn.players.Count,
            is18Plus = gn.is18Plus
        }).ToList();

        return View(model);
    }

    [Authorize]
    [HttpPost]
    public IActionResult SubmitReview(ReviewViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Haal de gegevens van de ingelogde gebruiker op
            var email = User.Identity.Name;
            var personId = _personsRepository.GetPersons().FirstOrDefault(p => p.email == email)?.personId;

            if (personId == null)
            {
                TempData["Error"] = "Je bent niet ingelogd. Log in om een review te schrijven.";
                return RedirectToAction("Login", "Login");
            }

            // Maak een nieuwe review aan
            var review = new Review
            {
                gameNightId = model.gameNightId,
                reviewerId = personId.Value,  // Veronderstelt dat personId een nullable int is
                rating = model.rating,
                comment = model.comment,

            };

            // Sla de review op in de database
            _reviewRepository.AddReview(review);


            // Redirect naar de GameNightInfo pagina
            TempData["Success"] = "Je review is succesvol ingediend!";
            return RedirectToAction("GameNightInfo", new { id = model.gameNightId });
        }

        // Als het model niet geldig is, geef de view opnieuw weer met foutmeldingen
        TempData["Error"] = "Er is iets mis met je review. Probeer het opnieuw.";
        return View("WriteReview", model);
    }
    [Authorize]
    [HttpGet]
    public IActionResult WriteReview(int id)
    {
        _logger.LogInformation($"WriteReview actie aangeroepen met gameNightId: {id}");

        var gameNight = _gameNightRepository.GetGameNight(id);
        var email = User.Identity!.Name;
        var person = _personsRepository.GetPersons().FirstOrDefault(p => p.email == email);

        if (person == null)
        {
            return Unauthorized();
        }

        var personId = person.personId;

        if (gameNight == null)
        {
            return NotFound("Game night niet gevonden." + id);
        }

        // Controleer of de GameNight al voorbij is
        if (gameNight.dateTime > DateTime.Now)
        {
            TempData["Error"] = "Je kunt geen review schrijven voor een game night die nog niet geweest is.";
            return RedirectToAction("GameNightInfo", new { id = id });
        }


        // Controleer of de gebruiker zich heeft ingeschreven voor de game night
        var isSignedUp = _gameNightRepository.isUserSignedUpForGameNight(personId, id);
        if (!isSignedUp)
        {
            TempData["Error"] = "Je kunt alleen een review schrijven voor een game night waaraan je hebt deelgenomen.";
            return RedirectToAction("GameNightInfo", new { id = id });
        }

        var model = new ReviewViewModel
        {
            gameNightId = id
        };

        return View(model);
    }
    [HttpPost]
    public IActionResult DeleteGameNight(int id)
    {
        Console.WriteLine("DeleteGameNight actie aangeroepen met gameNightId" + id);
        // Haal de spelavond op
        var gameNight = _gameNightRepository.GetGameNights()
            .Include(g => g.players)
            .FirstOrDefault(g => g.gameNightId == id);

        if (gameNight == null)
        {
            Console.WriteLine("Spelavond niet gevonden");
            TempData["ErrorMessage"] = "De spelavond kon niet worden gevonden.";
            return RedirectToAction("MyGameNights");
        }

        // Controleer of er spelers zijn ingeschreven
        if (gameNight.players != null && gameNight.players.Any())
        {
            TempData["ErrorMessage"] = "Je kunt een spelavond niet verwijderen als er spelers zijn ingeschreven.";
            return RedirectToAction("MyGameNights");
        }

        // Verwijder de spelavond
        _gameNightRepository.DeleteGameNight(id);

        TempData["SuccessMessage"] = "De spelavond is succesvol verwijderd.";
        return RedirectToAction("MyGameNights");
    }
    [Authorize]
    [HttpGet]
   public IActionResult EditGameNight(int id)
{
    // Haal de game night op
    var gameNight = _gameNightRepository.GetGameNights()
        .Include(g => g.games)  // Zorg ervoor dat je de spellen ook ophaalt
        .Include(g => g.players)
        .FirstOrDefault(g => g.gameNightId == id);

    if (gameNight == null)
    {
        TempData["ErrorMessage"] = "De spelavond kan niet gevonden worden.";
        return RedirectToAction("MyGameNights");
    }

    if (gameNight.dateTime < DateTime.Now)
    {
        TempData["ErrorMessage"] = "Je kunt een spelavond niet bewerken als deze al is begonnen.";
        return RedirectToAction("MyGameNights");
    }

    if (gameNight.players.Any())
    {
        TempData["ErrorMessage"] = "Je kunt een spelavond niet bewerken als er al spelers zijn ingeschreven.";
        return RedirectToAction("MyGameNights");
    }

    // Haal de lijst van spellen op
    var games = _gameRepository.GetGames();
    if (games == null || !games.Any())
    {
        TempData["ErrorMessage"] = "Er zijn geen spellen beschikbaar.";
        return RedirectToAction("MyGameNights");
    }

    // Verkrijg de geselecteerde spellen
    var selectedGames = gameNight.games?.Select(g => g.gameId).ToList() ?? new List<int>();

    // Stel ViewBag in voor de MultiSelectList
    ViewBag.Games = new MultiSelectList(games, "gameId", "name", selectedGames);

    // Maak een model
    var model = new GameNightModel
    {
        gameNightId = id,
        dateTime = gameNight.dateTime,
        address = gameNight.address,
        maxPlayers = gameNight.maxPlayers,
        is18Plus = gameNight.is18Plus,
        lactoseFree = gameNight.lactoseFree,
        nutFree = gameNight.nutFree,
        vegetarian = gameNight.vegetarian,
        alcoholic = gameNight.alcoholic,
        food = gameNight.food?.Split(", ")?.ToList() ?? new List<string>(),
        games = selectedGames
    };

    return View(model);
}

    [HttpPost]
    public IActionResult EditGameNight(GameNightModel model)
    {
        if (!ModelState.IsValid)
        {
            var games = _gameRepository.GetGames();
            ViewBag.Games = new MultiSelectList(games, "GameId", "Name");
            return View(model);
        }
        Console.WriteLine("EditGameNight actie aangeroepen met gameNightId" + model.gameNightId);

        var gameNight = _gameNightRepository.GetGameNight(model.gameNightId);
        if (gameNight == null)
        {
            TempData["ErrorMessage"] = "De spelavond kan niet worden gevonden.";
            return RedirectToAction("Index");
        }

        // Update the game night properties
        gameNight.dateTime = model.dateTime;
        gameNight.address = model.address;
        gameNight.maxPlayers = model.maxPlayers;
        gameNight.is18Plus = model.is18Plus;
        gameNight.lactoseFree = model.lactoseFree;
        gameNight.nutFree = model.nutFree;
        gameNight.vegetarian = model.vegetarian;
        gameNight.alcoholic = model.alcoholic;
        gameNight.food = string.Join(", ", model.food);

        // Update the games
        var selectedGames = _gameRepository.GetGames().Where(g => model.games.Contains(g.gameId)).ToList();
        gameNight.games = selectedGames;

        _gameNightRepository.UpdateGameNight(gameNight);

        TempData["SuccessMessage"] = "De spelavond is succesvol bijgewerkt.";
        return RedirectToAction("MyGameNights");
    }
    

}


