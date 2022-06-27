using System.Collections.Generic;
using ChessEngine.Common;
using ChessEngine.Interfaces;
using ChessEngine.Pieces;
using ChessEngine.tests.Fixtures;
using FluentAssertions;
using Moq;
using Xunit;

namespace ChessEngine.tests
{
    public class EnPassantTests
    {
        private EnPassant enPassant;
        private static Mock<IPlayer> playerOne = new Mock<IPlayer>();
        private static Mock<IPlayer> playerTwo = new Mock<IPlayer>();
        private Mock<IPawn> mockPawn;
        private Mock<ISquare> mockNewSquare;
        private Mock<IPawn> otherPawn;
        private Mock<ISquare> otherPawnSquare;
        public static IEnumerable<object[]> TestData = new List<object[]>
        {
            new object[] {playerOne.Object, ('d', '2'), ('d', '4'), playerTwo.Object, ('e', '4'), ('d', '3') },
            new object[] {playerTwo.Object, ('d', '7'), ('d', '5'), playerOne.Object, ('e', '5'), ('d', '6') },
            new object[] {playerTwo.Object, ('a', '7'), ('a', '5'), playerOne.Object, ('b', '5'), ('a', '6') }

        };

        public EnPassantTests()
        {
            enPassant = new EnPassant
            {
                Squares = MockBoard.MockSquares
            };

            playerOne.Setup(p => p.IsPlayer).Returns("One");

            playerTwo.Setup(p => p.IsPlayer).Returns("Two");

            mockPawn = new Mock<IPawn>();
            mockNewSquare = new Mock<ISquare>();
            otherPawn = new Mock<IPawn>();
            otherPawnSquare = new Mock<ISquare>();
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void CheckEnPassant_WhenPawnAdvancesTwoSquaresOnFirstMoveAndOpponentsPawnOnAdjacentFile_ThenOpponentsPawnGivenEnPassant(IPlayer firstActionPlayer, (char,char) firstPlayerPosition, (char,char) enPassantMovePosition, IPlayer secondEnPasantPlayer, (char,char)secondPlayerPosition, (char,char) expectedPosition)
        {
            string secondPositionId = $"{secondPlayerPosition.Item1}{secondPlayerPosition.Item2}";

            mockPawn.Setup(p => p.Player).Returns(firstActionPlayer);
            mockPawn.Setup(p => p.Position).Returns(firstPlayerPosition);

            mockNewSquare.Setup(s => s.Position).Returns(enPassantMovePosition);

            otherPawn.Setup(p => p.Player).Returns(secondEnPasantPlayer);
            otherPawn.Setup(p => p.Position).Returns(secondPlayerPosition);
            otherPawn.Setup(p => p.GetType()).Returns(typeof(Pawn));
            otherPawn.SetupProperty(p => p.EnPassant);

            otherPawnSquare.Setup(s => s.Piece).Returns(otherPawn.Object);
            otherPawnSquare.Setup(s => s.Occupied).Returns(true);
            otherPawnSquare.Setup(s => s.Position).Returns(secondPlayerPosition);
            otherPawnSquare.Setup(s => s.Id).Returns(secondPositionId);
            enPassant.Squares[secondPositionId] = otherPawnSquare.Object;

            enPassant.CheckEnPassant(mockPawn.Object, mockNewSquare.Object);

            otherPawn.Object.EnPassant.pieceToCapture.Should().Be(mockPawn.Object);
            otherPawn.Object.EnPassant.squareToMove.Position.Should().Be(expectedPosition);
        }
    }

}