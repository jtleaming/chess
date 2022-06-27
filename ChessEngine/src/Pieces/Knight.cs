using System;
using System.Collections.Generic;
using System.Linq;
using ChessEngine.Interfaces;

namespace ChessEngine.Pieces
{
    public class Knight : Piece
    {
        public Knight(ISquare currentSquare, IPlayer player) : base(currentSquare, player)
        {
        }

        protected override bool CheckRules(ISquare newSquare)
        {
            var ranksToMove = Math.Abs((int)newSquare.Position.rank - (int)Square.Position.rank);
            var filesToMove = Math.Abs((int)newSquare.Position.file- (int)Square.Position.file);
            List<bool> rules = new List<bool>
            {
                newSquare.Position.file == Square.Position.file,
                newSquare.Position.rank == Square.Position.rank,
                ranksToMove > 2,
                filesToMove > 2,
                ranksToMove + filesToMove == 2
            };

            return rules.Any(r => r);
        }
    }
}