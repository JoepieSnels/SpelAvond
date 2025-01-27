using IndividueleCSharpProject.Domain;
using IndividueleCSharpProject.DomainServices.Repositories;
using Moq;


public class GameNightRepositoryTests
{
    private readonly Mock<IGameNightRepository> _mockRepository;

    public GameNightRepositoryTests()
    {
        _mockRepository = new Mock<IGameNightRepository>();
    }




    [Fact]
    public void AddGame_ShouldAddGameToGameNight()
    {
        // Arrange
        var gameNight = new GameNight(
            host: 1,
            address: "123 Game Street",
            dateTime: DateTime.Now,
            lactoseFree: true,
            alcoholic: false,
            nutFree: true,
            vegetarian: false,
            maxPlayers: 10,
            is18Plus: true,
            games: new List<Games>(),
            players: new List<Person>()
        );

        var newGame = new Games("Codenames", "yippy", Genrè.action, false, "codenames.jpg", TypeOfGame.cardgame);

        // Act
        gameNight.games.Add(newGame);

        // Assert
        Assert.Single(gameNight.games);
        Assert.Contains(newGame, gameNight.games);
    }

    [Fact]
    public void AddReview_ShouldAddReviewToGameNight()
    {
        // Arrange
        var gameNight = new GameNight(
            host: 1,
            address: "123 Game Street",
            dateTime: DateTime.Now,
            lactoseFree: true,
            alcoholic: false,
            nutFree: true,
            vegetarian: false,
            maxPlayers: 10,
            is18Plus: false,
            games: new List<Games>(),
            players: new List<Person>()
        );

        var reviewer = new Person(5, "Chris", "Evans", new DateTime(1980, 6, 13), "chris.evans@example.com", "Maple Lane", "Los Angeles", "505", Gender.Male, false, true, false, false);
        var review = new Review(5, "Great event!", reviewer.personId, 1);

        // Act
        gameNight.reviews.Add(review);

        // Assert
        Assert.Single(gameNight.reviews);
        Assert.Contains(review, gameNight.reviews);
        Assert.Equal(5, review.rating);
        Assert.Equal("Great event!", review.comment);
    }

    [Fact]
    public void GetGameNights_ShouldReturnGameNights()
    {
        // Arrange
        var gameNight = new GameNight(
            host: 1,
            address: "123 Game Street",
            dateTime: DateTime.Now,
            lactoseFree: true,
            alcoholic: false,
            nutFree: true,
            vegetarian: false,
            maxPlayers: 10,
            is18Plus: true,
            games: new List<Games>(),
            players: new List<Person>()
        );

        // Act
        _mockRepository.Setup(x => x.GetGameNights()).Returns(new List<GameNight> { gameNight }.AsQueryable());

        // Assert
        var gameNights = _mockRepository.Object.GetGameNights();
        Assert.Single(gameNights);
        Assert.Contains(gameNight, gameNights);
    }

    [Fact]
    public void GetGameNight_ShouldReturnGameNight()
    {
        // Arrange
        var gameNight = new GameNight(
            host: 1,
            address: "123 Game Street",
            dateTime: DateTime.Now,
            lactoseFree: true,
            alcoholic: false,
            nutFree: true,
            vegetarian: false,
            maxPlayers: 10,
            is18Plus: true,
            games: new List<Games>(),
            players: new List<Person>()
        );

        // Act
        _mockRepository.Setup(x => x.GetGameNight(1)).Returns(gameNight);

        // Assert
        var retrievedGameNight = _mockRepository.Object.GetGameNight(1);
        Assert.Equal(gameNight, retrievedGameNight);
    }

   [Fact]
public void UpdateGameNight_ShouldUpdateGameNight()
{
    // Arrange
    var gameNight = new GameNight(
        gameNightId: 1,
        host: 1,
        address: "123 Game Street",
        dateTime: DateTime.Now,
        lactoseFree: true,
        alcoholic: false,
        nutFree: true,
        vegetarian: false,
        maxPlayers: 10,
        is18Plus: true,
        games: new List<Games>(),
        players: new List<Person>()
    );

    // Configureer de mock
    _mockRepository.Setup(x => x.UpdateGameNight(It.IsAny<GameNight>()));

    // Act
    _mockRepository.Object.UpdateGameNight(gameNight); 
    // Assert
    _mockRepository.Verify(x => x.UpdateGameNight(gameNight), Times.Once);
}
[Fact]
public void DeleteGameNight_ShouldDeleteGameNight()
{
    // Arrange
    var gameNight = new GameNight(
        gameNightId: 1,
        host: 1,
        address: "123 Game Street",
        dateTime: DateTime.Now,
        lactoseFree: true,
        alcoholic: false,
        nutFree: true,
        vegetarian: false,
        maxPlayers: 10,
        is18Plus: true,
        games: new List<Games>(),
        players: new List<Person>()
    );

    // Configureer de mock
    _mockRepository.Setup(x => x.DeleteGameNight(1));

    // Act
    _mockRepository.Object.DeleteGameNight(1);

    // Assert   
    _mockRepository.Verify(x => x.DeleteGameNight(1), Times.Once);
}



}
