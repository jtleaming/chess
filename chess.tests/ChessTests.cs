using System;
using System.Collections.Generic;
using Xunit;
using chess;

namespace chess.tests
{
    public class ChessTests
    {
        private readonly Pawn pawn;
        private readonly Move move;
        public ChessTests()
        {
            var e2 = new Square("E2");
            this.pawn = new Pawn(e2);
            this.move = new Move();
        }
        [Fact]
        public void Move_ShouldSetPawnLocation_WhenPieceIsMoved()
        {
            //Given
            var e4 = new Square("E4");
            //When
            move.Piece(pawn, e4);
            //Then
            Assert.Equal(e4, pawn.CurrentPosition);
        }

        [Fact]
        public void Move_ShouldReturnError_WhenPawnMovedMoreThanOnePosition()
        {
            var e7 = new Square("E7");
            Assert.Throws<Exception>(() => move.Piece(pawn, e7));
        }
    }

}
