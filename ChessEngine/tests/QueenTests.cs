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
    public class QueenTests
    {
        private readonly Queen queen;
        private Mock<IPlayer> mockPlayer;
        private Mock<ISquare> mockNewSquare;
        private Mock<ISquare> mockCurrentSquare;

        public QueenTests()
        {
            mockCurrentSquare = new Mock<ISquare>();
            mockPlayer = new Mock<IPlayer>();
            mockNewSquare = new Mock<ISquare>();

            mockCurrentSquare.Setup(s => s.Position).Returns(('d', '4'));
            mockPlayer.Setup(p => p.Turn).Returns(true);

            queen = new Queen(mockCurrentSquare.Object, mockPlayer.Object);
            queen.TurnHandler += MockTurnEventListener;
        }
        private void MockTurnEventListener(object sender, TurnEventArgs e) { }
        public static IEnumerable<object[]> InvalidMoves =>
            new List<object[]>{new object[] { 'c', '6' }, new object[] { 'b', '7' }, new object[] { 'a', '6' }, new object[] { 'e', '8' }, new object[] { 'g', '2' } };

        [Theory]
        [MemberData(nameof(InvalidMoves))]
        public void Queen_WhenMoveRankAndFilesAreOdd_ShouldThrowInvalidMoveException(char file, char rank)
        {
            mockNewSquare.Setup(s => s.Position).Returns((file, rank));
            Assert.Throws<InvalidMoveException>(() => queen.Move(mockNewSquare.Object));
        }

        [Fact]
        public void Queen_MoveAnyFilesInSameRank_ShouldBeValidMove()
        {
            mockNewSquare.Setup(s => s.Position).Returns(('d', '7'));
            var validMove = Record.Exception(() =>  queen.Move(mockNewSquare.Object));
            Assert.Null(validMove);
        }
        [Fact]
        public void Queen_MoveAnyRanksInSameFile_ShouldBeValidMove()
        {
            mockNewSquare.Setup(s => s.Position).Returns(('h', '4'));
            var validMove = Record.Exception(() =>  queen.Move(mockNewSquare.Object));
            Assert.Null(validMove);
        }
    }
}