using System;
using System.Collections.Generic;
using ChessEngine.Interfaces;
using ChessEngine.Pieces;
using Moq;
using Xunit;

namespace ChessEngine.tests
{
    public class EnPassantTests
    {
        [Fact]
        public void CheckEnPassant_WhenPawnAdvancesTwoSquaresOnFirsstMoveAndOpponentsPawnOnAdjacentFile_ThenOpponentsPawnGivenEnPassant()
        {
            EnPassant enPassant = new EnPassant();

            Mock<IPlayer> playerOne = new Mock<IPlayer>();
            Mock<IPlayer> playerTwo = new Mock<IPlayer>();

            Mock<IPiece> mockPawn = new Mock<IPiece>();
            mockPawn.Setup(p => p.Position).Returns(('d','7'));
            mockPawn.Setup(p => p.Player).Returns(playerTwo.Object);

            Mock<ISquare> mockNewSquare = new Mock<ISquare>();
            mockNewSquare.Setup(s => s.Position).Returns(('d', '5'));

            Mock<IPiece> otherPawn = new Mock<IPiece>();
            otherPawn.Setup(p => p.Position).Returns(('e','5'));
            otherPawn.Setup(p => p.Player).Returns(playerOne.Object);
            otherPawn.SetupProperty(p => (Pawn)p.EnPassant);

            enPassant.CheckEnPassant(mockPawn.Object as Pawn, new Mock<ISquare>().Object);
        }
    }

    internal class EnPassant
    {
        public Dictionary<string,ISquare> Squares;
        public EnPassant()
        {
        }

        internal void CheckEnPassant(Pawn pawn, ISquare squaresToMove)
        {
            var firstAdjacentSquare = Squares[squaresToMove.Position.file.ToString()+(squaresToMove.Position.rank-1)];
            var secondAdjacentSquare = Squares[squaresToMove.Position.file.ToString()+(squaresToMove.Position.rank+1)];
            if(firstAdjacentSquare.Occupied && firstAdjacentSquare.Piece is Pawn)
            {
                if(firstAdjacentSquare.Piece.Player != pawn.Player)
                {
                    Pawn adjacentPawn = firstAdjacentSquare.Piece as Pawn;
                    adjacentPawn.EnPassant = (true, pawn, squaresToMove);
                }
            }
        }
    }
}