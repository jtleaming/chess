using System;
using ChessEngine.Common;
using ChessEngine.Exceptions;
using ChessEngine.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace ChessEngine.tests
{
    public class PawnTests
    {
        private Mock<ISquare> mockCurrentSquare;
        private Mock<IPlayer> mockPlayer;
        private Mock<ISquare> mockNewSquare;
        private Pawn pawn;

        public PawnTests()
        {
            mockCurrentSquare = new Mock<ISquare>();
            mockPlayer = new Mock<IPlayer>();
            mockNewSquare = new Mock<ISquare>();

            mockCurrentSquare.Setup(s => s.Position).Returns(("b", "2"));

            pawn = new Pawn(mockCurrentSquare.Object, mockPlayer.Object);
            pawn.TurnHandler += MockTurnEventListener;
        }

        private void MockTurnEventListener(object sender, TurnEventArgs e)
        {
        }

        [Fact]
        public void Pawn_WhenMoveRank_ShouldThrowInvalidMoveException()
        {
            mockNewSquare.Setup(s => s.Position).Returns(("a", "2"));
            mockPlayer.SetupProperty(p => p.Turn, true);

            Assert.Throws<InvalidMoveException>(() => pawn.Move(mockNewSquare.Object));
        }
        [Fact]
        public void Pawn_WhenMoveFileBack_ShouldThrowInvalidMoveException()
        {
            mockNewSquare.Setup(s => s.Position).Returns(("b", "1"));
            mockPlayer.SetupProperty(p => p.Turn, true);

            Assert.Throws<InvalidMoveException>(() => pawn.Move(mockNewSquare.Object));
        }
        [Fact]
        public void Pawn_AfterFirstMove_FirstMoveShouldBeFalse()
        {
            mockNewSquare.Setup(s => s.Position).Returns(("b", "3"));
            mockPlayer.SetupProperty(p => p.Turn, true);

            pawn.FirstMove.Should().BeTrue();

            pawn.Move(mockNewSquare.Object);

            pawn.FirstMove.Should().BeFalse();
        }
        [Fact]
        public void Pawn_WhenFirstMoveFalse_MoveTwoFileShouldThrowInvalidMoveException()
        {
            mockNewSquare.Setup(s => s.Position).Returns(("b", "3"));
            mockPlayer.SetupProperty(p => p.Turn, true);

            pawn.Move(mockNewSquare.Object);

            mockNewSquare.Setup(s => s.Position).Returns(("b", "5"));
            mockPlayer.SetupProperty(p => p.Turn, true);

            Assert.Throws<InvalidMoveException>(() => pawn.Move(mockNewSquare.Object));
        }

    }
}