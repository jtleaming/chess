using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ChessEngine.Interfaces;
using ChessEngine.Extensions;
using FluentAssertions;
using Moq;
using Xunit;
using ChessEngine.Exceptions;
using static ChessEngine.Common.Singletons;
using ChessEngine.Common;
using System;

namespace ChessEngine.tests
{
    public class PlayerTests
    {
        private Dictionary<string,ISquare> mockSquares;
        private Mock<ISquare> mockSquare;
        private IPlayer player;
        public PlayerTests()
        {
            mockSquares = new Dictionary<string,ISquare>();
            mockSquare = new Mock<ISquare>();
            mockSquares.AddMultiple(16, mockSquare.Object);
            player = new Player(mockSquares);
        }
        private void MockTurnListener(object sender, TurnEventArgs e)
        {
        }

        [Fact]
        public void Player_WhenCreated_ShouldHave16Pieces()
        {
            player.Pieces.Count.Should().Be(16);
        }
        [Fact]
        public void Player_FirstPiece_ShouldSquareA1()
        {
            mockSquare.SetupGet(a => a.Position).Returns(("a", "1"));
            player.Pieces.First().Position.Should().Be(("a", "1"));
        }
        [Fact]
        public void Player_LastPiece_ShouldSquareH2()
        {
            mockSquare.Setup(a => a.Position).Returns(("h", "2"));
            player.Pieces.Last().Position.Should().Be(("h", "2"));
        }
        [Fact]
        public void Player_WhenMovePieceTurnFalse_ThrowsException()
        {
            player.Turn = false;
            var newSquare = new Mock<ISquare>();
            newSquare.Setup(s => s.Position).Returns(("e", "4"));
            Assert.Throws<InvalidMoveException>(() => player.Pieces[1].Move(newSquare.Object));
        }
        [Fact]
        public void Player_WhenMovePieceTurnTrue_DoesNotThrowException()
        {
            player.Turn = true;
            var newSquare = new Mock<ISquare>();
            newSquare.Setup(s => s.Position).Returns(("e", "4"));
            player.Pieces[1].TurnHandler += MockTurnListener;
            var exception = Record.Exception(() => player.Pieces[1].Move(newSquare.Object));
            Assert.Null(exception);
        }

        [Fact]
        public void Player_WhenMovePieceToSquareOccupiedByCurrentPlayer_ThrowsException()
        {
            player.Turn = true;
            mockSquare = new Mock<ISquare>();
            mockSquare.Setup(s => s.Occupied).Returns(true);
            mockSquare.Setup(s => s.Piece.Player).Returns(player);
            Assert.Throws<InvalidMoveException>(() => player.Pieces[1].Move(mockSquare.Object));
        }
    }
}