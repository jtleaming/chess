using System;
using System.Collections.Generic;
using System.Linq;
using ChessEngine.Exceptions;
using ChessEngine.Interfaces;

namespace ChessEngine.Pieces

{
    public class Queen : Piece
    {

        public Queen(ISquare currentSquare, IPlayer player) : base(currentSquare, player)
        {
        }

        protected override bool CheckRules(ISquare newSquare)
        {

            var ranksToMove = Math.Abs((int)newSquare.Position.rank - (int)Square.Position.rank);
            var filesToMove = Math.Abs((int)newSquare.Position.file - (int)Square.Position.file);
            int totalMoves = filesToMove + ranksToMove;
            List<bool> rules = new List<bool>
            {
                (newSquare.Position.file != currentSquare.Position.file &&
                newSquare.Position.rank != currentSquare.Position.rank) &&
                totalMoves % 2 != 0
            };

            return rules.Any(r => r);
        }
    }
}