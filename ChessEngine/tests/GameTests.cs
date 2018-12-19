using System;
using System.Linq;
using ChessEngine.Interfaces;
using FluentAssertions;
using Xunit;

namespace ChessEngine.tests
{
    public class GameTests
    {
        private Game game = new Game();

        public GameTests()
        {
            game.CreateGame();
        }

        [Fact]
        public void CreateGame_WhenCalled_CreatesNewGameBoard()
        {
            game.Board.Should().BeOfType(typeof(Board));
        }
        [Fact]
        public void CreateGame_WhenCalled_ShouldAddTwoPlayers()
        {
            game.Players.Should().BeOfType(typeof((IPlayer, IPlayer)));
        }
        [Fact]
        public void CreateGame_PlayerOneTurn_ShouldBeTrue()
        {
            game.Players.PlayerOne.Turn.Should().BeTrue();
        }
        [Fact]
        public void CreateGame_PlayerTwoTurn_ShouldBeFalse()
        {
            game.Players.PlayerTwo.Turn.Should().Be(false);
        }
        [Fact]
        public void PlayersOneAndTwo_ShouldHave16PiecesEach()
        {
            game.Players.PlayerOne.Pieces.Count.Should().Be(16);
            game.Players.PlayerTwo.Pieces.Count.Should().Be(16);
        }
        [Fact]
        public void PlayerOne_FirstPiece_ShouldBeA1()
        {
            game.Players.PlayerOne.Pieces.First().Position.Should().Be(("a", "1"));
        }
        [Fact]
        public void PlayerTwo_LastPiece_ShouldBeH8()
        {
            game.Players.PlayerTwo.Pieces.Last().Position.Should().Be(("h", "8"));
        }
        [Fact]
        public void PlayerOne_WhenAfterMove_PlayerOneTurnShouldBeFalse()
        {
            game.Players.PlayerOne.Pieces[1].Move(game.Board.Squares["e7"]);
            game.Players.PlayerOne.Turn.Should().BeFalse();
        }
        [Fact]
        public void PlayerOne_WhenAfterMove_PlayerTwoTurnShouldBeTrue()
        {
            game.Players.PlayerOne.Pieces[1].Move(game.Board.Squares["e7"]);
            game.Players.PlayerTwo.Turn.Should().BeTrue();
        }
    }
}