using System.Collections.Generic;
using System.Linq;
using ChessEngine.Exceptions;
using ChessEngine.Interfaces;

namespace ChessEngine.Pieces
{
    public class Rook : Piece
    {
        private readonly IPlayer player;
        public override IPlayer Player => player;

        public override ISquare Square => base.Square;
        public Rook(ISquare currentSquare, IPlayer player) : base(currentSquare, player)
        {
            this.player = player;
        }

        public override void Move(ISquare newSquare)
        {
            if(CheckRules(newSquare))
            {
                throw new InvalidMoveException("Rook cannot make this move.");
            }
            base.Move(newSquare);
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