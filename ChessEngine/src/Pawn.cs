using System;
using ChessEngine.Exceptions;
using ChessEngine.Interfaces;

namespace ChessEngine
{
    public class Pawn : Piece
    {
        private IPlayer player;
        private bool firstMove = true;
        public bool FirstMove { get => firstMove; }
        public Pawn(ISquare currentSquare, IPlayer player) : base(currentSquare, player)
        {
            Square = currentSquare;
            this.player = player;
        }

        public override void Move(ISquare newSquare)
        {
            if ((newSquare.Position.file != base.Square.Position.file)
            || (Convert.ToInt32(newSquare.Position.rank) < Convert.ToInt32(base.Square.Position.rank))
            || (!firstMove && Convert.ToInt32(newSquare.Position.rank) > 1))
            {
                throw new InvalidMoveException();
            }

            base.Move(Square);

            if (firstMove) firstMove = false;
        }
    }
}