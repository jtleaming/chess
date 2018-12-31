using System.Linq;
using ChessEngine;
using ChessEngine.Exceptions;
using ChessEngine.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace GameEngine.tests
{
    public class BoardTests
    {
        private Board newBoard;

        public BoardTests()
        {
            newBoard = new Board();
        }

        [Fact]
        public void Board_WhenInstantiated_Has64Squares()
        {
            newBoard.Squares.Count.Should().Be(64);
        }
        [Fact]
        public void Board_FirstSquare_ShouldBePositionA1()
        {
            newBoard.Squares.First().Value.Position.Should().Be(('a', '1'));
        }
        [Fact]
        public void Board_LastSquare_ShouldBePositionH8()
        {
            newBoard.Squares.Last().Value.Position.Should().Be(('h', '8'));
        }
        [Fact]
        public void ChecForPiecesBetween_WhenGivenHorizontalLocations_ShouldThrowInvalidMoveExceptionIfAnySquaresBetweenAreOccupied()
        {
            newBoard.Squares["c1"].Piece = new Mock<IPiece>().Object;
            Assert.Throws<InvalidMoveException>(() => newBoard.CheckForPiecesBetween("a1", "d1"));
        }
        [Fact]
        public void CheckForPiecesBetween_WhenGivenDiagonalLocations_ShouldThrowInvalidMoveExceptionIfAnySquaresBetweenAreOccupied()
        {
            newBoard.Squares["c3"].Piece = new Mock<IPiece>().Object;
            Assert.Throws<InvalidMoveException>(() => newBoard.CheckForPiecesBetween("a1", "e5"));
        }
        [Fact]
        public void CheckForPiecesBetween_WhenGivenVerticalLocations_ShouldThrowInvalidMoveExceptionIfAnySquaresBetweenAreOccupied()
        {
            newBoard.Squares["a3"].Piece = new Mock<IPiece>().Object;
            Assert.Throws<InvalidMoveException>(() => newBoard.CheckForPiecesBetween("a1", "a5"));
        }
        [Fact]
        public void CheckForPiecesBetween_WhenGivenVerticalLocationsReverse_ShouldThrowInvalidMoveExceptionIfAnySquaresBetweenAreOccupied()
        {
            newBoard.Squares["a3"].Piece = new Mock<IPiece>().Object;
            Assert.Throws<InvalidMoveException>(() => newBoard.CheckForPiecesBetween("a5", "a1"));
        }
    }
}