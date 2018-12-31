using System.Collections.Generic;
using ChessEngine.Common;
using ChessEngine.Exceptions;
using ChessEngine.Interfaces;
using ChessEngine.Pieces;
using Moq;
using Xunit;

namespace ChessEngine.tests
{
    public class KingTests
    {
        private readonly King king;
        private Mock<IPlayer> mockPlayer;
        private Mock<ISquare> mockNewSquare;
        private Mock<ISquare> mockCurrentSquare;

        public KingTests()
        {
            mockCurrentSquare = new Mock<ISquare>();
            mockPlayer = new Mock<IPlayer>();
            mockNewSquare = new Mock<ISquare>();

            mockCurrentSquare.Setup(s => s.Position).Returns(('d', '4'));
            mockPlayer.Setup(p => p.Turn).Returns(true);

            king = new King(mockCurrentSquare.Object, mockPlayer.Object);
            king.TurnHandler += MockTurnEventListener;
        }
        private void MockTurnEventListener(object sender, TurnEventArgs e) { }
        public static IEnumerable<object[]> ValidMoves => new List<object[]>
        { new object[] {'c','4'}, new object[] {'c','5'},new object[] {'c','3'},new object[] {'d','5'},new object[] {'e','5'},new object[] {'e','4'},new object[] {'e','3'},new object[] {'d','3'} };

        [Fact]
        public void King_MoveMultipleRank_ShouldThrowInvalidMoveException()
        {
            mockNewSquare.Setup(s => s.Position).Returns(('d', '8'));
            Assert.Throws<InvalidMoveException>(() => king.Move(mockNewSquare.Object));
        }
        [Fact]
        public void King_MoveMultipleFile_ShouldThrowInvalidMoveException()
        {
            mockNewSquare.Setup(s => s.Position).Returns(('a', '4'));
            Assert.Throws<InvalidMoveException>(() => king.Move(mockNewSquare.Object));
        }
        [Theory]
        [MemberData(nameof(ValidMoves))]
        public void King_MoveOneRankOrFileAnyDirection_ShouldBeValidMove(char file, char rank)
        {
            mockNewSquare.Setup(s => s.Position).Returns((file, rank));
            var validMove = Record.Exception(() => king.Move(mockNewSquare.Object));
            Assert.Null(validMove);
        }
    }
}