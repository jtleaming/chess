using System.Collections.Generic;
using ChessEngine.Common;
using ChessEngine.Exceptions;
using ChessEngine.Interfaces;
using ChessEngine.Pieces;
using Moq;
using Xunit;

namespace ChessEngine.tests
{
    public class KnightTests
    {
        private readonly Knight knight;
        private Mock<IPlayer> mockPlayer;
        private Mock<ISquare> mockNewSquare;
        private Mock<ISquare> mockCurrentSquare;

        public KnightTests()
        {
            mockCurrentSquare = new Mock<ISquare>();
            mockPlayer = new Mock<IPlayer>();
            mockNewSquare = new Mock<ISquare>();

            mockCurrentSquare.Setup(s => s.Position).Returns(('d', '4'));
            mockPlayer.Setup(p => p.IsPlayer).Returns("One");
            mockPlayer.Setup(p => p.CapturedPieces).Returns(new List<IPiece>());
            mockPlayer.Setup(p => p.Turn).Returns(true);

            knight = new Knight(mockCurrentSquare.Object, mockPlayer.Object);
            knight.TurnHandler += MockTurnEventListener;
        }

        private void MockTurnEventListener(object sender, TurnEventArgs e) {}

        [Fact]
        public void Knight_WhenMoveToSameFile_ShouldThrowInvalidMoveException()
        {
            mockNewSquare.Setup(s => s.Position).Returns(('d', '5'));
            Assert.Throws<InvalidMoveException>(() => knight.Move(mockNewSquare.Object));
        }
        [Fact]
        public void Knight_WhenMoveToSameRank_ShouldThrowInvalidMoveException()
        {
            mockNewSquare.Setup(s => s.Position).Returns(('a', '4'));
            Assert.Throws<InvalidMoveException>(() => knight.Move(mockNewSquare.Object));
        }
        [Fact]
        public void Knight_WhenMoveRankGreaterThan2_ShouldThrowInvalidMoveException()
        {
            mockNewSquare.Setup(s => s.Position).Returns(('b', '1'));
            Assert.Throws<InvalidMoveException>(() => knight.Move(mockNewSquare.Object));
        }
        [Fact]
        public void Knight_WhenMoveFileGreaterThan2_ShouldThrowInvalidMoveException()
        {
            mockNewSquare.Setup(s => s.Position).Returns(('g', '5'));
            Assert.Throws<InvalidMoveException>(() => knight.Move(mockNewSquare.Object));
        }
        [Fact]
        public void Knight_WhenMoveDiagonally_ShouldThrowInvalidMoveException()
        {
            mockNewSquare.Setup(s => s.Position).Returns(('e', '5'));
            Assert.Throws<InvalidMoveException>(() => knight.Move(mockNewSquare.Object));
            mockNewSquare.Setup(s => s.Position).Returns(('c', '5'));
            Assert.Throws<InvalidMoveException>(() => knight.Move(mockNewSquare.Object));
            mockNewSquare.Setup(s => s.Position).Returns(('c', '3'));
            Assert.Throws<InvalidMoveException>(() => knight.Move(mockNewSquare.Object));
            mockNewSquare.Setup(s => s.Position).Returns(('e', '3'));
            Assert.Throws<InvalidMoveException>(() => knight.Move(mockNewSquare.Object));
        }
        [Theory]
        [MemberData(nameof(ValideMoves))]
        public void Knight_WhenCheckRulesReturnsFalse_ShouldReturnValidMove(char file, char rank)
        {
            mockNewSquare.Setup(s => s.Position).Returns((file, rank));
            var moveValid = Record.Exception(() =>  knight.Move(mockNewSquare.Object));
            Assert.Null(moveValid);
        }

        public static IEnumerable<object[]> ValideMoves() => 
            new List<object[]>
            {
                new object[] {'c','2'},
                new object[] {'b','3'},
                new object[] {'b','5'},
                new object[] {'c','6'},
                new object[] {'e','6'},
                new object[] {'f','5'},
                new object[] {'f','3'},
                new object[] {'e','2'}
            };
    }
}