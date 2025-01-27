using IndividueleCSharpProject.Domain;
using IndividueleCSharpProject.DomainServices.Repositories;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace IndividueleCSharpProject.Infrastructure
{
    public class PersonRepositoryEF : IPersonsRepository
    {
        private readonly GamenightDBContext _context;

        public PersonRepositoryEF(GamenightDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Person> GetPersons()
        {
            return _context.Persons.ToList();

        }

        public Person GetPerson(int id)
        {
            return _context.Persons.FirstOrDefault(p => p.personId == id);
        }

        public void AddPerson(Person person)
        {
            _context.Persons.Add(person);
            _context.SaveChanges();
        }

        public void UpdatePerson(Person person)
        {
            _context.Persons.Update(person);
            _context.SaveChanges();
        }

        public void DeletePerson(int id)
        {
            Person person = _context.Persons.FirstOrDefault(p => p.personId == id);
            _context.Persons.Remove(person);
            _context.SaveChanges();
        }
        
    }
}   