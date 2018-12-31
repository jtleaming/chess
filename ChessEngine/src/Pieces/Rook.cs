using System.Collections.Generic;
using System.Linq;
using ChessEngine.Exceptions;
using ChessEngine.Interfaces;

namespace ChessEngine.Pieces
{
    public class Rook : Piece
    {
        public Rook(ISquare currentSquare, IPlayer player) : base(currentSquare, player)
        {
        }

        protected override bool CheckRules(ISquare newSquare)
        {
            List<bool> rules = new List<bool>
            {
                newSquare.Position.file != currentSquare.Position.file &&
                newSquare.Position.rank != currentSquare.Position.rank
            };

            return rules.Any(r => r);
        }
    }
}