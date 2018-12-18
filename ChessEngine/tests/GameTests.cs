using ChessEngine.Interfaces;
using FluentAssertions;
using Xunit;

namespace ChessEngine.tests
{
    public class GameTests
    {
        [Fact]
        public void CreateGame_WhenCalled_CreatesNewGameBoard()
        {
            var newGame = new Game();
            newGame.CreateGame();

            newGame.Board.Should().BeOfType(typeof(Board));
        }
        [Fact]
        public void CreateGame_WhenCalled_ShouldAddTwoPlayers()
        {
            var newGame = new Game();
            newGame.CreateGame();

            newGame.Players.Should().BeOfType(typeof(Players));
        }
    }
}