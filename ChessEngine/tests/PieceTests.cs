using System;
using ChessEngine.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace ChessEngine.tests
{
    public class PieceTests
    {
        [Fact]
        public void Move_ShouldBecomeNewSquarePiece_WhenMovedToNewSquare()
        {
            var currentSquare = new Mock<ISquare>().SetupProperty(p => p.Piece);
            var piece = new Piece(currentSquare.Object);
            var newSquare = new Mock<ISquare>().SetupProperty(p => p.Piece);

            piece.Move(newSquare.Object);

            newSquare.Object.Piece.Should().Be(piece);
        }
        [Fact]
        public void Move_ShouldNotBeOriginalSquarePiece_WhenPieceMovedToNewSquare()
        {
            var originalSquare = new Mock<ISquare>().SetupProperty(p => p.Piece);
            var piece = new Piece(originalSquare.Object);
            var newSquare = new Mock<ISquare>().SetupProperty(p => p.Piece);

            piece.Move(newSquare.Object);

            originalSquare.Object.Piece.Should().NotBe(piece);
        }
        [Fact]
        public void Position_ShouldBeSquarePostion_WhenPieceAssignedToSquare()
        {
            (string,string) position = ("a", "1");
            var originalSquare = new Mock<ISquare>(); 
            originalSquare.Setup(p => p.Position).Returns(position);
            var piece = new Piece(originalSquare.Object);

            piece.Postion.Should().Be(position);
        }
        [Fact]
        public void Move_ShouldThrowException_WhenSquareIsOutsideBoardRange()
        {
        }
    }
}