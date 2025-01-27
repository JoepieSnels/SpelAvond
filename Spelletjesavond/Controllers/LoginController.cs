using IndividueleCSharpProject.Domain;
using IndividueleCSharpProject.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spelletjesavond.Models;

namespace IndividueleCSharpProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly GamenightDBContext _gamenightContext;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public LoginController(
        GamenightDBContext gamenightContext,
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
    {
        _gamenightContext = gamenightContext;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    // Acties zoals Login
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.password))
            {
                await _signInManager.SignInAsync(user, isPersistent: model.rememberMe);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Ongeldige inloggegevens.");
        }

        return View(model);
    }

        // Logout: verwerk uitlogverzoek
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Login");
        }

        // Register GET: toon de registratiepagina
        [HttpGet]
        public IActionResult MakeAccount()
        {
            return View();
        }

        // Register POST: verwerk registreren
       [HttpPost]
        public async Task<IActionResult> MakeAccount(MakeAccountModel model)
        {
            if (ModelState.IsValid)
        {     
        // Maak een nieuwe IdentityUser voor de registratie
        var user = new IdentityUser
        {
            UserName = model.email,
            Email = model.email
        };

        // Maak het account aan met het opgegeven wachtwoord
        var result = await _userManager.CreateAsync(user, model.password);

        // Controleer of het aanmaken van de IdentityUser succesvol was
        if (result.Succeeded)
        {
            // Voeg de persoon toe aan de Person tabel (GamenightDBContext)
            var person = new Person(
                model.firstName,
                model.lastName,
                model.birthDate,
                model.email, // Zorg ervoor dat de email overeenkomt
                model.street,
                model.city,
                model.houseNumber,
                model.gender,
                model.lactoseFree,
                model.alcoholic,
                model.nutFree,
                model.vegetarian
            );

            _gamenightContext.Persons.Add(person);
            await _gamenightContext.SaveChangesAsync();  // Zorg ervoor dat de persoon wordt opgeslagen in de database

            // Log de gebruiker in na succesvolle registratie
            await _signInManager.SignInAsync(user, isPersistent: false);

            // Redirect naar de homepagina of een andere gewenste pagina
            return RedirectToAction("Login", "Login");
        }
        else
        {
            // Als er fouten zijn bij het aanmaken van de IdentityUser, voeg ze dan toe aan de ModelState
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }

    // Als het model niet geldig is of als er een fout was, geef de view opnieuw weer met de fouten
    return View(model);
}

    }
}
