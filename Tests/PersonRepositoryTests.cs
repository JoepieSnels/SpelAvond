using IndividueleCSharpProject.Domain;
using IndividueleCSharpProject.DomainServices.Repositories;
using Moq;

namespace IndividueleCSharpProject.Tests{
    public class PersonRepositoryTests{
        private Mock<IPersonsRepository> _mockRepo;
        public PersonRepositoryTests(){
            _mockRepo = new Mock<IPersonsRepository>();
        }

        [Fact]
        public void AddPerson_ShouldAddPerson(){
            // Arrange
            var person = new Person(
                personId: 1,
                firstName: "John",
                lastName: "Doe",
                email: "OwKb1@example.com",
                birthDate: new DateTime(1999, 12, 31),
                street: "FakeStreet",
                city: "Springfield",
                houseNumber: "123",
                gender: Gender.Male,
                lactoseFree: true,
                alcoholic: false,
                nutFree: true,
                vegatarian: false
            );

            // Act
            _mockRepo.Setup(x => x.AddPerson(person));
            _mockRepo.Object.AddPerson(person);

            // Assert
            _mockRepo.Verify(x => x.AddPerson(person), Times.Once);
        }

        [Fact]
        public void GetPerson_ShouldReturnPerson(){
            // Arrange
            var person = new Person(
                personId: 1,
                firstName: "John",
                lastName: "Doe",
                email: "OwKb1@example.com",
                birthDate: new DateTime(1999, 12, 31),
                street: "FakeStreet",
                city: "Springfield",
                houseNumber: "123",
                gender: Gender.Male,
                lactoseFree: true,
                alcoholic: false,
                nutFree: true,
                vegatarian: false
            );

            // Act
            _mockRepo.Setup(x => x.GetPerson(1)).Returns(person);
            var result = _mockRepo.Object.GetPerson(1);

            // Assert
            Assert.Equal(person, result);
        }

        [Fact]
        public void GetPersons_ShouldReturnPersons(){
            // Arrange
            var person = new Person(
                personId: 1,
                firstName: "John",
                lastName: "Doe",
                email: "OwKb1@example.com",
                birthDate: new DateTime(1999, 12, 31),
                street: "FakeStreet",
                city: "Springfield",
                houseNumber: "123",
                gender: Gender.Male,
                lactoseFree: true,
                alcoholic: false,
                nutFree: true,
                vegatarian: false
            );

            // Act
            _mockRepo.Setup(x => x.GetPersons()).Returns(new List<Person> { person }.AsQueryable());
            var result = _mockRepo.Object.GetPersons();

            // Assert
            Assert.Single(result);
            Assert.Contains(person, result);
        }

        [Fact]
        public void UpdatePerson_ShouldUpdatePerson(){
            // Arrange
            var person = new Person(
                personId: 1,
                firstName: "John",
                lastName: "Doe",
                email: "OwKb1@example.com",
                birthDate: new DateTime(1999, 12, 31),
                street: "FakeStreet",
                city: "Springfield",
                houseNumber: "123",
                gender: Gender.Male,
                lactoseFree: true,
                alcoholic: false,
                nutFree: true,
                vegatarian: false
            );

            // Act
            _mockRepo.Setup(x => x.UpdatePerson(person));
            _mockRepo.Object.UpdatePerson(person);

            // Assert
            _mockRepo.Verify(x => x.UpdatePerson(person), Times.Once);
        }

        [Fact]
        public void DeletePerson_ShouldDeletePerson(){
            // Arrange
            var person = new Person(
                personId: 1,
                firstName: "John",
                lastName: "Doe",
                email: "OwKb1@example.com",
                birthDate: new DateTime(1999, 12, 31),
                street: "FakeStreet",
                city: "Springfield",
                houseNumber: "123",
                gender: Gender.Male,
                lactoseFree: true,
                alcoholic: false,
                nutFree: true,
                vegatarian: false
            );

            // Act
            _mockRepo.Setup(x => x.DeletePerson(1));
            _mockRepo.Object.DeletePerson(1);

            // Assert
            _mockRepo.Verify(x => x.DeletePerson(1), Times.Once);
        }
    }

}