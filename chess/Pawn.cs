using System;
using System.Linq;
using chess;

namespace chess
{
    public class Pawn
    {
        public Square StartingPosition;
        public Square CurrentPosition;
        public Square[] AvailableMoves { get; set; }

        public Pawn(Square StartingPosition)
        {
            this.StartingPosition = StartingPosition;
            SetCurrentPosition(StartingPosition);
            SetAvailableMoves(StartingPosition);
        }

        public void UpdateAvailableMoves(Square square)
        {
            square.row++;
            AvailableMoves = new[] { square };
        }

        private void SetCurrentPosition(Square startingPosition)
        {
            this.CurrentPosition = StartingPosition;
        }

        private void SetAvailableMoves(Square startingPosition)
        {
            if (startingPosition.row == 2)
            {
                AvailableMoves = new[] { Board.Squares[2], Board.Squares[3] };
            }
        }
    }
}