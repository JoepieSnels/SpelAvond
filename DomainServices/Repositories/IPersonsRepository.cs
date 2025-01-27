namespace IndividueleCSharpProject.DomainServices.Repositories
{
    using System.Collections.Generic;
    using IndividueleCSharpProject.Domain;
    public interface IPersonsRepository
    {
        public IEnumerable<Person> GetPersons();
        public Person GetPerson(int id);
        public void AddPerson(Person person);
        public void UpdatePerson(Person person);
        public void DeletePerson(int id);
    }
}
