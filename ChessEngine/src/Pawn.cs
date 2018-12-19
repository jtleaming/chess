using System;
using ChessEngine.Exceptions;
using ChessEngine.Interfaces;

namespace ChessEngine
{
    public class Pawn : Piece
    {

        public Pawn(ISquare currentSquare, IPlayer player) : base(currentSquare, player)
        {
            base.Square = currentSquare;
        }


        public override void Move(ISquare newSquare)
        {
            if((newSquare.Position.rank != base.Square.Position.rank)
                || (Convert.ToInt32(newSquare.Position.file) < Convert.ToInt32(base.Square.Position.file)) )
            {
                throw new InvalidMoveException();
            }
            base.Move(Square);
        }
    }
}