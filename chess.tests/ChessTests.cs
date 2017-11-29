using System;
using System.Collections.Generic;
using Xunit;
using chess;

namespace chess.tests
{
    public class ChessTests
    {
        [Fact]
        public void Move_ShouldSetPawnLocation_WhenPieceIsMoved()
        {
            //Given
            var pawn = new Pawn("E2");
            var move = new Move();
            //When
            move.Piece(pawn, "E4");
            //Then
            Assert.Equal("E4", String.Concat(pawn.CurrentPosition));
        }

        [Fact]
        public void Move_ShouldReturnError_WhenPawnMovedMoreThanOnePosition()
        {
            //Given
            var move = new Move();
            var pawn = new Pawn("E4");
            //Then
            Assert.Throws<Exception>(() => move.Piece(pawn,"E7"));
        }
        [Fact (Skip="Waiting to refactor")]
        public void Move_ShouldNotReturnError_WhenPawnMovedMoreThanOnePositionOnFirstMove()
        {
            //Given
            var move = new Move();
            var pawn = new Pawn("E2");
            //Then
            Assert.Throws<Exception>(() => move.Piece(pawn,"E4"));
        }
    }

}
