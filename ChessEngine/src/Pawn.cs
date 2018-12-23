using System;
using System.Collections.Generic;
using System.Linq;
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
            var squaresToMove = Char.GetNumericValue(newSquare.Position.rank) - Char.GetNumericValue(Square.Position.rank);
            if (CheckRules(newSquare))
            {
                throw new InvalidMoveException();
            }

            base.Move(newSquare);

            if (firstMove) firstMove = false;
        }

        private bool CheckRules(ISquare newSquare)
        {
            var squaresToMove = Char.GetNumericValue(newSquare.Position.rank) - Char.GetNumericValue(Square.Position.rank);
            List<bool> rules = new List<bool>
            {
                (player.IsPlayer == "One") ? firstMove && squaresToMove > 2 : firstMove && squaresToMove < 2, 
                (player.IsPlayer == "One") ? !firstMove && squaresToMove > 1 : !firstMove && squaresToMove < 1,
                (player.IsPlayer == "One") ? newSquare.Position.rank < Square.Position.rank : newSquare.Position.rank > Square.Position.rank,
                newSquare.Position.file > Square.Position.file + 1 || newSquare.Position.file < Square.Position.file - 1 ? true : newSquare.Piece?.Player == player,
                newSquare.Position.rank == Square.Position.rank
            };

            return rules.Any(r => r);
        }
    }
}