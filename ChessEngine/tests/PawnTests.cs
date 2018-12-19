using System;
using ChessEngine.Common;
using ChessEngine.Exceptions;
using ChessEngine.Interfaces;
using Moq;
using Xunit;

namespace ChessEngine.tests
{
    public class PawnTests
    {
        private Mock<ISquare> mockCurrentSquare;
        private Mock<IPlayer> mockPlayer;
        private Mock<ISquare> mockNewSquare;

        public PawnTests()
        {
            mockCurrentSquare = new Mock<ISquare>();
            mockPlayer = new Mock<IPlayer>();
            mockNewSquare = new Mock<ISquare>();

            mockCurrentSquare.Setup(s => s.Position).Returns(("b", "2"));
        }

        private void MockTurnEventListener(object sender, TurnEventArgs e)
        {
        }

        [Fact]
        public void Move_WhenPawnMoveRank_ShouldThrowInvalidMoveException()
        {
            mockNewSquare.Setup(s => s.Position).Returns(("a", "2"));
            mockPlayer.SetupProperty(p => p.Turn, true);

            var pawn = new Pawn(mockCurrentSquare.Object, mockPlayer.Object);
            pawn.TurnHandler += MockTurnEventListener;

            Assert.Throws<InvalidMoveException>(() => pawn.Move(mockNewSquare.Object));
        }
        [Fact]
        public void Move_WhenPawnMoveFileBack_ShouldThrowInvalidMoveException()
        {
            mockNewSquare.Setup(s => s.Position).Returns(("b", "1"));
            mockPlayer.SetupProperty(p => p.Turn, true);

            var pawn = new Pawn(mockCurrentSquare.Object, mockPlayer.Object);
            pawn.TurnHandler += MockTurnEventListener;

            Assert.Throws<InvalidMoveException>(() => pawn.Move(mockNewSquare.Object));
        }

    }
}