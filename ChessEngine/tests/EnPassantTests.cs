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
        [Fact]
        public void CheckEnPassant_WhenPawnAdvancesTwoSquaresOnFirstMoveAndOpponentsPawnOnAdjacentFile_ThenOpponentsPawnGivenEnPassant()
        {
            EnPassant enPassant = new EnPassant
            {
                Squares = MockBoard.MockSquares
            };

            Mock<IPlayer> playerOne = new Mock<IPlayer>();
            Mock<IPlayer> playerTwo = new Mock<IPlayer>();

            Mock<IPawn> mockPiece = new Mock<IPawn>();
            mockPiece.Setup(p => p.Position).Returns(('d', '7'));
            mockPiece.Setup(p => p.Player).Returns(playerTwo.Object);

            Mock<ISquare> mockNewSquare = new Mock<ISquare>();
            mockNewSquare.Setup(s => s.Position).Returns(('d', '5'));

            Mock<IPawn> otherPiece = new Mock<IPawn>();
            otherPiece.Setup(p => p.Position).Returns(('e', '5'));
            otherPiece.Setup(p => p.Player).Returns(playerOne.Object);
            otherPiece.Setup(p => p.GetType()).Returns(typeof(Pawn));
            otherPiece.SetupProperty(p => p.EnPassant);

            var squareE5 = new Mock<ISquare>();
            squareE5.Setup(s => s.Piece).Returns(otherPiece.Object);
            squareE5.Setup(s => s.Occupied).Returns(true);
            squareE5.Setup(s => s.Position).Returns(('e', '5'));
            squareE5.Setup(s => s.Id).Returns("e5");
            enPassant.Squares["e5"] = squareE5.Object;

            enPassant.CheckEnPassant(mockPiece.Object, mockNewSquare.Object);

            Assert.True(otherPiece.Object.EnPassant.canEnPassant);
            otherPiece.Object.EnPassant.pieceToCapture.Should().Be(mockPiece.Object);

        }
    }

}