using System;
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
        private Mock<IPlayer> playerOne;
        private Mock<IPlayer> playerTwo;
        private Mock<IPawn> mockPawn;
        private Mock<ISquare> mockNewSquare;
        private Mock<IPawn> otherPawn;
        private Mock<ISquare> otherPawnSquare;

        public EnPassantTests()
        {
            enPassant = new EnPassant
            {
                Squares = MockBoard.MockSquares
            };

            playerOne = new Mock<IPlayer>();
            playerOne.Setup(p => p.IsPlayer).Returns("One");

            playerTwo = new Mock<IPlayer>();
            playerTwo.Setup(p => p.IsPlayer).Returns("Two");

            mockPawn = new Mock<IPawn>();
            mockNewSquare = new Mock<ISquare>();
            otherPawn = new Mock<IPawn>();
            otherPawnSquare = new Mock<ISquare>();
        }

        [Fact]
        public void CheckEnPassant_WhenPawnAdvancesTwoSquaresOnFirstMoveAndOpponentsPawnOnAdjacentFile_ThenOpponentsPawnGivenEnPassant()
        {
            mockPawn.Setup(p => p.Position).Returns(('d', '7'));
            mockPawn.Setup(p => p.Player).Returns(playerTwo.Object);

            mockNewSquare.Setup(s => s.Position).Returns(('d', '5'));

            otherPawn.Setup(p => p.Position).Returns(('e', '5'));
            otherPawn.Setup(p => p.Player).Returns(playerOne.Object);
            otherPawn.Setup(p => p.GetType()).Returns(typeof(Pawn));
            otherPawn.SetupProperty(p => p.EnPassant);

            otherPawnSquare.Setup(s => s.Piece).Returns(otherPawn.Object);
            otherPawnSquare.Setup(s => s.Occupied).Returns(true);
            otherPawnSquare.Setup(s => s.Position).Returns(('e', '5'));
            otherPawnSquare.Setup(s => s.Id).Returns("e5");
            enPassant.Squares["e5"] = otherPawnSquare.Object;

            enPassant.CheckEnPassant(mockPawn.Object, mockNewSquare.Object);

            otherPawn.Object.EnPassant.pieceToCapture.Should().Be(mockPawn.Object);
            otherPawn.Object.EnPassant.squareToMove.Position.Should().Be(('d','6'));

        }
        [Fact]
        public void OtherCheckEnPassant_WhenPawnAdvancesTwoSquaresOnFirstMoveAndOpponentsPawnOnAdjacentFile_ThenOpponentsPawnGivenEnPassant()
        {
            mockPawn.Setup(p => p.Position).Returns(('d', '2'));
            mockPawn.Setup(p => p.Player).Returns(playerOne.Object);

            mockNewSquare.Setup(s => s.Position).Returns(('d', '4'));

            otherPawn.Setup(p => p.Position).Returns(('e', '4'));
            otherPawn.Setup(p => p.Player).Returns(playerTwo.Object);
            otherPawn.Setup(p => p.GetType()).Returns(typeof(Pawn));
            otherPawn.SetupProperty(p => p.EnPassant);

            otherPawnSquare.Setup(s => s.Piece).Returns(otherPawn.Object);
            otherPawnSquare.Setup(s => s.Occupied).Returns(true);
            otherPawnSquare.Setup(s => s.Position).Returns(('e', '4'));
            otherPawnSquare.Setup(s => s.Id).Returns("e4");
            enPassant.Squares["e4"] = otherPawnSquare.Object;

            enPassant.CheckEnPassant(mockPawn.Object, mockNewSquare.Object);

            otherPawn.Object.EnPassant.pieceToCapture.Should().Be(mockPawn.Object);
            otherPawn.Object.EnPassant.squareToMove.Position.Should().Be(('d','3'));

        }
    }

}