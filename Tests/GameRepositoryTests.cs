using IndividueleCSharpProject.Domain;
using IndividueleCSharpProject.DomainServices.Repositories;
using Moq;

namespace IndividueleCSharpProject.Tests
{
    public class GameRepositoryTests
    {
        private readonly Mock<IGameRepository> _mockRepository;

        public GameRepositoryTests()
        {
            _mockRepository = new Mock<IGameRepository>();
        }

        [Fact]
        public void GetGame_ShouldReturnGame()
        {
            // Arrange
            var game = new Games(
                gameId: 1,
                name: "Game 1",
                description: "Description 1",
                image: "image1.jpg",
                is18Plus: true,
                genre: Genrè.action,
                typeOfGame: TypeOfGame.boardgame
            );

            // Act
            _mockRepository.Setup(x => x.GetGame(1)).Returns(game);
            var result = _mockRepository.Object.GetGame(1);

            // Assert
            Assert.Equal(game, result);
        }

        [Fact]
        public void GetGames_ShouldReturnGames()
        {
            // Arrange
            var game = new Games(
                gameId: 1,
                name: "Game 1",
                description: "Description 1",
                image: "image1.jpg",
                is18Plus: true,
                genre: Genrè.action,
                typeOfGame: TypeOfGame.boardgame
            );

            // Act
            _mockRepository.Setup(x => x.GetGames()).Returns(new List<Games> { game }.AsQueryable()); ;
            var result = _mockRepository.Object.GetGames();

            // Assert
            Assert.Single(result);
            Assert.Contains(game, result);
        }

        [Fact]
        public void AddGame_ShouldAddGame()
        {
            // Arrange
            var game = new Games(
                gameId: 1,
                name: "Game 1",
                description: "Description 1",
                image: "image1.jpg",
                is18Plus: true,
                genre: Genrè.action,
                typeOfGame: TypeOfGame.boardgame
            );

            // Act
            _mockRepository.Setup(x => x.AddGame(game));
            _mockRepository.Object.AddGame(game);

            // Assert
            _mockRepository.Verify(x => x.AddGame(game), Times.Once);
        }

        [Fact]
        public void DeleteGame_ShouldDeleteGame()
        {
            // Arrange
            var gameId = 1;

            // Act
            _mockRepository.Setup(x => x.DeleteGame(gameId));
            _mockRepository.Object.DeleteGame(gameId);

            // Assert
            _mockRepository.Verify(x => x.DeleteGame(gameId), Times.Once);
        }
        
    }
    

}