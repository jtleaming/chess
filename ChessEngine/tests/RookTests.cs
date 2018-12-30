using System;
using System.Collections.Generic;
using ChessEngine.Common;
using ChessEngine.Exceptions;
using ChessEngine.Interfaces;
using ChessEngine.Pieces;
using Moq;
using Xunit;

namespace ChessEngine.tests
{
    public class RookTests
    {
        private readonly Rook rook;
        private Mock<IPlayer> mockPlayer;
        private Mock<ISquare> mockNewSquare;
        private Mock<ISquare> mockSquare;
        private Mock<ISquare> mockCurrentSquare;

        public RookTests()
        {
            mockCurrentSquare = new Mock<ISquare>();
            mockPlayer = new Mock<IPlayer>();
            mockNewSquare = new Mock<ISquare>();

            mockCurrentSquare.Setup(s => s.Position).Returns(('a', '1'));
            mockPlayer.Setup(p => p.IsPlayer).Returns("One");
            mockPlayer.Setup(p => p.CapturedPieces).Returns(new List<IPiece>());
            mockPlayer.Setup(p => p.Turn).Returns(true);

            rook = new Rook(mockCurrentSquare.Object, mockPlayer.Object);
            rook.TurnHandler += MockTurnEventListener;
        }

        private void MockTurnEventListener(object sender, TurnEventArgs e) {}
        [Theory]
        [InlineData('b','2')]
        [InlineData('h','8')]
        [InlineData('f','6')]
        public void Rook_WhenMoveBothRankAndFile_ThrowsInvalidMoveException(char file, char rank)
        {
            mockNewSquare.Setup(s => s.Position).Returns((file, rank));
            Assert.Throws<InvalidMoveException>(() => rook.Move(mockNewSquare.Object));
        }
        [Fact]
        public void Rook_WhenMoveAnyNumberRank_ShouldBeValidMove()
        {
            mockNewSquare.Setup(s => s.Position).Returns(('a','8'));
            var validMove = Record.Exception(() => rook.Move(mockNewSquare.Object));
            Assert.Null(validMove);
        }
        [Fact]
        public void Rook_WhenMoveAnyNumberFile_ShouldBeValidMove()
        {
            mockNewSquare.Setup(s => s.Position).Returns(('h','1'));
            var validMove = Record.Exception(() => rook.Move(mockNewSquare.Object));
            Assert.Null(validMove);
        }
    }
}