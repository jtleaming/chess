using System;
using System.Collections.Generic;
using ChessEngine.Common;
using ChessEngine.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace ChessEngine.tests
{
    public class PieceTests
    {
        private Mock<IPlayer> player;
        public PieceTests()
        {
            player = new Mock<IPlayer>();
            player.Setup(p => p.Turn).Returns(true);
        }
        private void MockEventListener(object o, TurnEventArgs eventArgs)
        {
        }

        [Fact]
        public void Move_ShouldBecomeNewSquarePiece_WhenMovedToNewSquare()
        {
            var currentSquare = new Mock<ISquare>().SetupProperty(p => p.Piece);
            var piece = new Piece(currentSquare.Object, player.Object);
            var newSquare = new Mock<ISquare>().SetupProperty(p => p.Piece);
            piece.TurnHandler += MockEventListener;
            piece.Move(newSquare.Object);

            newSquare.Object.Piece.Should().Be(piece);
        }
        [Fact]
        public void Move_ShouldNotBeOriginalSquarePiece_WhenPieceMovedToNewSquare()
        {
            var originalSquare = new Mock<ISquare>().SetupProperty(p => p.Piece);
            var piece = new Piece(originalSquare.Object, player.Object);
            var newSquare = new Mock<ISquare>().SetupProperty(p => p.Piece);
            piece.TurnHandler += MockEventListener;

            piece.Move(newSquare.Object);

            originalSquare.Object.Piece.Should().NotBe(piece);
        }
        [Fact]
        public void Position_ShouldBeSquarePostion_WhenPieceAssignedToSquare()
        {
            (string, string) position = ("a", "1");
            var originalSquare = new Mock<ISquare>();
            originalSquare.Setup(p => p.Position).Returns(position);
            var piece = new Piece(originalSquare.Object, player.Object);

            piece.Position.Should().Be(position);
        }
        [Fact]
        public void Move_ShouldAddOccupyingPieceToPlayerCapturedPieces_WhenNewSquareOccupiedByOtherPlayer()
        {
            var currentSquare = new Mock<ISquare>().SetupProperty(p => p.Piece);
            var piece = new Piece(currentSquare.Object, player.Object);
            piece.TurnHandler += MockEventListener;

            var mockPiece = new Mock<IPiece>();
            var mockPlayer = new Mock<IPlayer>();
            var newSquare = new Mock<ISquare>();

            player.Setup(p => p.CapturedPieces).Returns(new List<IPiece>());

            mockPiece.Setup(p => p.Player).Returns(mockPlayer.Object);
            mockPiece.Setup(p => p.Square).Returns(newSquare.Object);

            mockPlayer.Setup(p => p.Pieces).Returns(new List<IPiece>{mockPiece.Object});

            newSquare.Setup(s => s.Piece).Returns(mockPiece.Object);
            newSquare.SetupProperty(s => s.Occupied, true);

            piece.Move(newSquare.Object);
            player.Object.CapturedPieces.Count.Should().Be(1);
        }
    }
}