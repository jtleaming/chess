using System;
using System.Collections.Generic;
using System.Linq;
using ChessEngine.Exceptions;
using ChessEngine.Interfaces;

namespace ChessEngine.Pieces
{
    public class Bishop : Piece
    {
        private readonly IPlayer player;

        public override IPlayer Player => player;

        public override ISquare Square => base.Square;
        public Bishop(ISquare currentSquare, IPlayer player) : base(currentSquare, player)
        {
            this.player = player;
        }

        protected override bool CheckRules(ISquare newSquare)
        {
            var ranksToMove = Math.Abs((int)newSquare.Position.rank - (int)Square.Position.rank);
            var filesToMove = Math.Abs((int)newSquare.Position.file - (int)Square.Position.file);
            int totalMoves = filesToMove + ranksToMove;
            List<bool> rules = new List<bool>
            {
                totalMoves % 2 != 0,
                newSquare.Position.rank == currentSquare.Position.rank,
                newSquare.Position.file == currentSquare.Position.file,

            };

            return rules.Any(r => r);
        }
    }
}