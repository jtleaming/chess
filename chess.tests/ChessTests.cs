using System;
using System.Collections.Generic;
using Xunit;
using chess;
using System.Linq;

namespace chess.tests
{
    public class ChessTests
    {
        private readonly Pawn pawn;
        private readonly Move move;
        public ChessTests()
        {
            this.pawn = new Pawn(Board.Squares.FirstOrDefault(s => s.name == "E2"));
            this.move = new Move();
        }
        [Fact]
        public void Move_ShouldSetPawnLocation_WhenPieceIsMoved()
        {
            //When
            move.Piece(pawn, Board.Squares.FirstOrDefault(s => s.name == "E4"));
            //Then
            Assert.Equal(Board.Squares.FirstOrDefault(s => s.name == "E5"), pawn.CurrentPosition);
        }

        [Fact]
        public void Move_ShouldReturnError_WhenPawnMovedMoreThanOnePosition()
        {
            Assert.Throws<Exception>(() => move.Piece(pawn, Board.Squares.FirstOrDefault(s => s.name == "E7")));
        }
    }

}
