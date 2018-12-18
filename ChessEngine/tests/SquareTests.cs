using ChessEngine;
using ChessEngine.Exceptions;
using ChessEngine.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace ChessEngine.tests
{
    public class SquareTests
    {
        [Fact]
        public void Square_WhenInsnantiated_HasRankAndFile()
        {
            var square = new Square("a", "1");
            square.Position.rank.Should().NotBeNull();
            square.Position.file.Should().NotBeNull();
        }
        [Fact]
        public void Square_Position_ShouldBeRankAndFile()
        {
            var square = new Square("a", "1");
            square.Position.ToString().Should().Be("a1");
        }
        [Fact]
        public void Square_WhenHasPiece_OccupiedShouldBeTrue()
        {
            var square = new Square("a", "1");
            square.Piece = new Mock<IPiece>().Object;
            square.Occupied.Should().Be(true);
        }
        [Fact]
        public void Square_WhenHasNoPiece_OccupiedShouldBeFalse()
        {
            var square = new Square("a", "1");
            square.Occupied.Should().BeFalse();
        }
        [Fact]
        public void Square_WhenConstructedWithInvalidRank_ShouldThrowInvalidRankException()
        {
            Assert.Throws<InvalidRankException>(() => { new Square("something", "1"); });
        }
        [Fact]
        public void Square_WhenConstructedWithInvalidFile_ShouldThrowInvalidFileException()
        {
            Assert.Throws<InvalidFileException>(() => { new Square("a", "something"); });
        }

    }
}