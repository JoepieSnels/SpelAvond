using System.ComponentModel.DataAnnotations;

namespace Spelletjesavond.Models{
    public class LoginModel{
        [Required(ErrorMessage = "Vul uw email in")]
        public string? email { get; set; }
        [Required(ErrorMessage = "Vul uw wachtwoord in")]
        public string? password { get; set; }

        public bool rememberMe { get; set; }

    }
}