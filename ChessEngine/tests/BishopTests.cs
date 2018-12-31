using System.Collections.Generic;
using ChessEngine.Common;
using ChessEngine.Exceptions;
using ChessEngine.Interfaces;
using ChessEngine.Pieces;
using Moq;
using Xunit;

namespace ChessEngine.tests
{
    public class BishopTests
    {
        private readonly Bishop bishop;
        private Mock<IPlayer> mockPlayer;
        private Mock<ISquare> mockNewSquare;
        private Mock<ISquare> mockCurrentSquare;

        public BishopTests()
        {
            mockCurrentSquare = new Mock<ISquare>();
            mockPlayer = new Mock<IPlayer>();
            mockNewSquare = new Mock<ISquare>();

            mockCurrentSquare.Setup(s => s.Position).Returns(('d', '4'));
            mockPlayer.Setup(p => p.Turn).Returns(true);

            bishop = new Bishop(mockCurrentSquare.Object, mockPlayer.Object);
            bishop.TurnHandler += MockTurnEventListener;
        }
        private void MockTurnEventListener(object sender, TurnEventArgs e) { }
        public static IEnumerable<object[]> ValidMoves =>
            new List<object[]>
            {
                new object[] {'c', '5'}, new object[] {'b', '6'}, new object[] {'a', '7'}, new object[] {'e', '3'}, new object[] {'g', '7'}, new object[] {'g', '1'}
            };
        public static IEnumerable<object[]> InvalidMoves =>
            new List<object[]>
            {
                new object[] {'c', '6'}, new object[] {'b', '7'}, new object[] {'a', '6'}, new object[] {'e', '8'}, new object[] {'g', '2'}, new object[] {'d', '8'}
            };

        [Theory]
        [MemberData(nameof(InvalidMoves))]
        public void Bishop_WhenMoveNonDiagonally_ShouldThrowInvalidMoveExcetion(char file, char rank)
        {
            mockNewSquare.Setup(s => s.Position).Returns((file, rank));
            Assert.Throws<InvalidMoveException>(() => bishop.Move(mockNewSquare.Object));
        }
        [Theory]
        [MemberData(nameof(ValidMoves))]
        public void Bishop_WhenMoveDiagonally_ShouldBeValidMove(char file, char rank)
        {
            mockNewSquare.Setup(s => s.Position).Returns((file, rank));
            var validMove = Record.Exception(() => bishop.Move(mockNewSquare.Object));
            Assert.Null(validMove);
        }
    }
}