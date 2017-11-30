using System;

namespace chess
{
    public class Move
    {
        public Move() { }
        public void Piece(Pawn piece, Square square)
        {
            if (square.row >= piece.CurrentPosition.row + 3)
            {
                throw new Exception();
            }
            piece.CurrentPosition = square;
            piece.UpdateAvailableMoves(square);
        }
    }

}