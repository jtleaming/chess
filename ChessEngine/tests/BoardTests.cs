using System.Linq;
using ChessEngine;
using FluentAssertions;
using Xunit;

namespace GameEngine.tests
{
    public class BoardTests
    {
        [Fact]
        public void Board_WhenInstantiated_Has64Squares()
        {
            Board newBoard = new Board();

            newBoard.Squares.Count.Should().Be(64);
        }
        [Fact]
        public void Board_FirstSquare_ShouldBePositionA1()
        {
            Board newBoard = new Board();
            newBoard.Squares.First().Position.Should().Be(("a","1"));
        }
        [Fact]
        public void Board_LastSquare_ShouldBePositionH8()
        {
            Board newBoard = new Board();
            newBoard.Squares.Last().Position.Should().Be(("h","8"));
        }
    }
}