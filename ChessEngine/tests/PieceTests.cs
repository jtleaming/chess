using System;
using System.Collections.Generic;
using ChessEngine.Common;
using ChessEngine.Exceptions;
using ChessEngine.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace ChessEngine.tests
{
    public class PieceTests
    {
        private Mock<IPlayer> player;
        private Piece piece;
        private Mock<ISquare> currentSquare;
        private Mock<ISquare> mockNewSquare;
        private readonly (char, char) position = ('a', '1');
        public PieceTests()
        {
            player = new Mock<IPlayer>();
            player.Setup(p => p.Turn).Returns(true);


            mockNewSquare = new Mock<ISquare>();
            mockNewSquare.Setup(s => s.Position).Returns(('e', '1'));

            currentSquare = new Mock<ISquare>().SetupProperty(p => p.Piece);
            currentSquare.Setup(s => s.Position).Returns(position);
            piece = new Piece(currentSquare.Object, player.Object);
            piece.TurnHandler += MockEventListener;
        }
        private void MockEventListener(object o, TurnEventArgs eventArgs)
        {
        }

        [Fact]
        public void Move_ShouldBecomeNewSquarePiece_WhenMovedToNewSquare()
        {
            var newSquare = new Mock<ISquare>().SetupProperty(p => p.Piece);
            piece.TurnHandler += MockEventListener;
            piece.Move(newSquare.Object);

            newSquare.Object.Piece.Should().Be(piece);
        }
        [Fact]
        public void Move_ShouldNotBeOriginalSquarePiece_WhenPieceMovedToNewSquare()
        {
            var newSquare = new Mock<ISquare>().SetupProperty(p => p.Piece);
            piece.TurnHandler += MockEventListener;

            piece.Move(newSquare.Object);

            currentSquare.Object.Piece.Should().NotBe(piece);
        }
        [Fact]
        public void Position_ShouldBeSquarePostion_WhenPieceAssignedToSquare()
        {
            piece.Position.Should().Be(position);
        }
        [Fact]
        public void Move_ShouldAddOccupyingPieceToPlayerCapturedPieces_WhenNewSquareOccupiedByOtherPlayer()
        {

            var mockPiece = new Mock<IPiece>();
            var mockPlayer = new Mock<IPlayer>();
            var newSquare = new Mock<ISquare>();

            player.Setup(p => p.CapturedPieces).Returns(new List<IPiece>());

            mockPiece.Setup(p => p.Player).Returns(mockPlayer.Object);
            mockPiece.Setup(p => p.Square).Returns(newSquare.Object);

            mockPlayer.Setup(p => p.Pieces).Returns(new List<IPiece> { mockPiece.Object });

            newSquare.Setup(s => s.Piece).Returns(mockPiece.Object);
            newSquare.SetupProperty(s => s.Occupied, true);

            piece.Move(newSquare.Object);
            player.Object.CapturedPieces.Count.Should().Be(1);
        }

        [Fact]
        public void Move_WhenPiecesPlayerTurnFalse_ThrowsException()
        {
            player.Setup(p => p.Turn).Returns(false);
            Assert.Throws<InvalidMoveException>(() => piece.Move(mockNewSquare.Object));
        }
        [Fact]
        public void Player_WhenMovePieceTurnTrue_DoesNotThrowException()
        {
            var exception = Record.Exception(() => piece.Move(mockNewSquare.Object));
            Assert.Null(exception);
        }

        [Fact]
        public void Player_WhenMovePieceToSquareOccupiedByCurrentPlayer_ThrowsException()
        {
            mockNewSquare.Setup(a => a.Occupied).Returns(true);
            mockNewSquare.Setup(s => s.Piece.Player).Returns(player.Object);
            Assert.Throws<InvalidMoveException>(() => piece.Move(mockNewSquare.Object));
        }
    }
}