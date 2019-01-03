using System;
using System.Collections.Generic;
using System.Linq;
using ChessEngine.Exceptions;
using ChessEngine.Interfaces;

namespace ChessEngine.Pieces
{
    public class King : Piece
    {
        private bool castling;
        public King(ISquare currentSquare, IPlayer player) : base(currentSquare, player)
        {
        }

        public override void Move(ISquare newSquare)
        {
            castling = newSquare.Piece?.GetType() == typeof(Rook) && newSquare.Piece?.Player == Player && newSquare.Piece.FirstMove && FirstMove;

            if(castling)
            {
                var previousSquare = currentSquare;
                var rook = newSquare.Piece;
                newSquare.Piece = null;

                base.Move(newSquare);

                previousSquare.Piece = rook;
            }
            else
            {
                base.Move(newSquare);
            }

        }

        protected override bool CheckRules(ISquare newSquare)
        {

            var ranksToMove = Math.Abs((int)newSquare.Position.rank - (int)Square.Position.rank);
            var filesToMove = Math.Abs((int)newSquare.Position.file - (int)Square.Position.file);
            int totalMoves = filesToMove + ranksToMove;
            List<bool> rules = new List<bool>
            {
                totalMoves > 2 ?
                !castling :
                false
            };

            return rules.Any(r => r);
        }
    }
}