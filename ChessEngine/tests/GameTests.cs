using System;
using System.Linq;
using ChessEngine.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace ChessEngine.tests
{
    public class GameTests
    {
        private Game game;
        private Mock<IEnPassant> enPassant = new Mock<IEnPassant>(); 

        public GameTests()
        {
            game = new Game();
            game.CreateGame(enPassant.Object);
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
            game.Players.PlayerOne.Pieces.First().Position.Should().Be(('a', '1'));
            game.Players.PlayerOne.Pieces.Last().Position.Should().Be(('h', '2'));
        }
        [Fact]
        public void PlayerTwo_LastPiece_ShouldBeH8()
        {
            game.Players.PlayerTwo.Pieces.First().Position.Should().Be(('h', '8'));
            game.Players.PlayerTwo.Pieces.Last().Position.Should().Be(('a', '7'));
        }
        [Fact]
        public void PlayerOne_WhenAfterMove_PlayerOneTurnShouldBeFalse()
        {
            game.Players.PlayerOne.Move("a2 a3");
            game.Players.PlayerOne.Turn.Should().BeFalse();
        }
        [Fact]
        public void PlayerOne_WhenAfterMove_PlayerTwoTurnShouldBeTrue()
        {
            game.Players.PlayerOne.Move("d2 d3");
            game.Players.PlayerTwo.Turn.Should().BeTrue();
        }
        [Fact]
        public void PlayerOne_WhenCapturesPlayerTwoPiece_PlayerTwoShouldHave15Pieces()
        {
            game.Players.PlayerOne.Move("d2 d4");
            game.Players.PlayerTwo.Move("e7 e5");
            game.Players.PlayerOne.Move("d4 e5");
            game.Players.PlayerTwo.Pieces.Count.Should().Be(15);
        }
        [Fact]
        public void PlayerOne_WhenCapturesPlayerTwoPiece_PlayerOneShouldHave1CapturePiece()
        {
            game.Players.PlayerOne.Move("d2 d4");
            game.Players.PlayerTwo.Move("e7 e5");
            game.Players.PlayerOne.Move("d4 e5");
            game.Players.PlayerOne.CapturedPieces.Count.Should().Be(1);
        }
        [Fact]
        public void PlayerOne_WhenCallsMove_PieceShouldMoveToNewSquare()
        {
            game.Players.PlayerOne.Move("b2 b3");

            game.Board.Squares.First(s => s.Value.Id == "b2").Value.Occupied.Should().BeFalse();
            game.Board.Squares.First(s => s.Value.Id == "b3").Value.Occupied.Should().BeTrue();
        }
        [Fact]
        public void EnPassantListener_WhenPawnMovesOnFirstMove_ShouldCheckForEnPassant()
        {
            game.Players.PlayerOne.Move("b2 b3");

            enPassant.Verify(e => e.CheckEnPassant(It.IsAny<IPawn>(), It.IsAny<ISquare>()));
        }
    }
}