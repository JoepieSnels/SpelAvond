
using Microsoft.AspNetCore.Identity;

namespace IndividueleCSharpProject.Domain
{
    public class Person
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
        public ICollection<GameNight>? gameNights { get; set; } = new List<GameNight>();
        public bool lactoseFree { get; set; } = false;
        public bool alcoholic { get; set; } = false;
        public bool nutFree { get; set; } = false;
        public bool vegatarian { get; set; } = false;

        public ICollection<Review>? reviews { get; set; } = new List<Review>();
        
        public Person(int personId, string firstName, string lastName)
        {
            this.personId = personId;
            this.firstName = firstName;
            this.lastName = lastName;

        }


        public Person(string firstName, string lastName, DateTime birthDate, string email, string street, string city, string houseNumber, Gender gender, bool lactoseFree, bool alcoholic, bool nutFree, bool vegatarian)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.birthDate = birthDate;
            this.email = email;
            this.street = street;
            this.city = city;
            this.houseNumber = houseNumber;
            this.gender = gender;
            this.lactoseFree = lactoseFree;
            this.alcoholic = alcoholic;
            this.nutFree = nutFree;
            this.vegatarian = vegatarian;
           
        }
        public Person(int personId, string firstName, string lastName, DateTime birthDate, string email, string street, string city, string houseNumber, Gender gender, bool lactoseFree, bool alcoholic, bool nutFree, bool vegatarian) : this(firstName, lastName, birthDate, email, street, city, houseNumber, gender, lactoseFree, alcoholic, nutFree, vegatarian) 
        {
            this.personId = personId;

        } // Zorg ervoor dat deze naam overeenkomt in je hele projecT
    }
}
