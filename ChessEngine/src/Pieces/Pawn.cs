using System;
using System.Collections.Generic;
using System.Linq;
using ChessEngine.Exceptions;
using ChessEngine.Interfaces;

namespace ChessEngine.Pieces
{
    public class Pawn : Piece, IPawn
    {
        public Pawn(ISquare currentSquare, IPlayer player) : base(currentSquare, player)
        {
        }

        public (bool canEnPassant, IPawn pieceToCapture, ISquare squareToMove) EnPassant { get; set; }

        protected override bool CheckRules(ISquare newSquare)
        {
            var squaresToMove = Math.Abs((int)newSquare.Position.rank - (int)Square.Position.rank);
            int filesToMove = Math.Abs((int)newSquare.Position.file - (int)Square.Position.file);
            List<bool> rules = new List<bool>
            {
                FirstMove && squaresToMove > 2,
                !FirstMove && squaresToMove > 1,
                (Player.IsPlayer == "One") ? newSquare.Position.rank < Square.Position.rank : newSquare.Position.rank > Square.Position.rank,
                filesToMove > 1,
                filesToMove == 1 && (newSquare.Piece?.Player == Player || !newSquare.Occupied),
                newSquare.Position.rank == Square.Position.rank
            };

            return rules.Any(r => r);
        }
    }
}