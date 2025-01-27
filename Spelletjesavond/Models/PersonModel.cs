using IndividueleCSharpProject.Domain;

namespace Spelletjesavond.Models
{
    public class PersonModel
    {
      public int personId { get; set; } // Zorg ervoor dat deze naam overeenkomt in je hele project
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime birthDate { get; init; }
        public string email { get; init; }
        public string street { get; set; }
        public string city { get; set; }
        public string houseNumber { get; set; }
        public Gender gender { get; set; }
        public bool lactoseFree { get; set; } = false;
        public bool alcoholic { get; set; } = false;
        public bool nutFree { get; set; } = false;
        public bool vegatarian { get; set; } = false;
        

    }
}