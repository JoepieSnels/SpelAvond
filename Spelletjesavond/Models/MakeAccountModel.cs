using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Spelletjesavond.Models
{
    public class MakeAccountModel
    {
        [Required(ErrorMessage = "Het emailveld is verplicht.")]
        [EmailAddress(ErrorMessage = "Ongeldig emailadres.")]
        public string email { get; set; }

        [Required(ErrorMessage = "Het wachtwoordveld is verplicht.")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required(ErrorMessage = "Het herhalen van het wachtwoord is verplicht.")]
        [Compare("password", ErrorMessage = "De wachtwoorden komen niet overeen.")]
        [DataType(DataType.Password)]
        public string repeatPassword { get; set; }

        [Required(ErrorMessage = "Het voornaamveld is verplicht.")]
        public string firstName { get; set; }

        [Required(ErrorMessage = "Het achternaamveld is verplicht.")]
        public string lastName { get; set; }

        [Required(ErrorMessage = "Het straatveld is verplicht.")]
        public string street { get; set; }

        [Required(ErrorMessage = "Het huisnummer is verplicht.")]
        public string houseNumber { get; set; }

        [Required(ErrorMessage = "Het plaatsveld is verplicht.")]
        public string city { get; set; }

        [Required(ErrorMessage = "De geboortedatum is verplicht.")]
        [DataType(DataType.Date)]
        public DateTime birthDate { get; set; }

        [Required(ErrorMessage = "Het geslacht is verplicht.")]
        public Gender gender { get; set; }

        public bool lactoseFree { get; set; }
        public bool alcoholic { get; set; }
        public bool nutFree { get; set; }
        public bool vegetarian { get; set; }
    }
}
