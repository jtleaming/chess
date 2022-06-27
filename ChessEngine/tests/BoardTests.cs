using System.Linq;
using ChessEngine;
using FluentAssertions;
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
    }
}