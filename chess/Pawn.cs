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
            square.name = string.Concat(square.column.ToString() + square.row.ToString());
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
                var positionOne = new Square() {column = startingPosition.column, row = startingPosition.row + 1, name = string.Concat(startingPosition.column.ToString() + (startingPosition.row + 1).ToString())};
                var positionTwo = new Square() {column = startingPosition.column, row = startingPosition.row + 2 , name = string.Concat(startingPosition.column.ToString() + (startingPosition.row + 2).ToString())};
                AvailableMoves = new[] { positionOne, positionTwo };
            }
        }
    }
}