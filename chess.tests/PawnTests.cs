using System.Linq;
using Xunit;

namespace chess.tests
{
    public class PawnTests
    {
        [Fact]
        public void Pawn_ShouldReturnAvailableMoves_WhenNewPawnIsCreated()
        {
            //Given
            var pawn = new Pawn(Board.Squares[1]);
            //When
            var validMoves = pawn.AvailableMoves;
            var expected = new [] { Board.Squares[2], Board.Squares[3] };
            //Then
            Assert.Equal(expected.First().name, validMoves.First().name);
        }

        [Fact]
        public void Pawn_ShouldUpdateAvailableMoves_WhenPawnMoves()
        {
            //Given
            var pawn = new Pawn(Board.Squares[1]);
            var move = new Move();
            //When
            move.Piece(pawn, Board.Squares[3]);
            //Then
            Assert.Equal(Board.Squares[4].name, pawn.AvailableMoves[0].name);
        }
    }

}