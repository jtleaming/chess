using System;

namespace chess
{
    public class Move
    {
        public Move(){}
        public void Piece(Pawn piece, string v2)
        {
            var newPosition = Int32.Parse(v2.ToCharArray()[1].ToString());
            var currentPosition = Int32.Parse(piece.CurrentPosition[1].ToString());

            if(newPosition >= currentPosition + 3)
            {
                throw new Exception();
            }

            piece.CurrentPosition = v2.ToCharArray();
        }
    }
}