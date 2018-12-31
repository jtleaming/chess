using System;
using System.Collections.Generic;
using System.Linq;
using ChessEngine.Exceptions;
using ChessEngine.Interfaces;

namespace ChessEngine.Pieces
{
    public class King : Piece
    {
        private readonly IPlayer player;

        public override IPlayer Player => player;
        public override ISquare Square { get => base.currentSquare; }
        public King(ISquare currentSquare, IPlayer player) : base(currentSquare, player)
        {
            this.currentSquare = currentSquare;
            this.player = player;
        }

        public override void Move(ISquare newSquare)
        {
            if(CheckRules(newSquare))
            {
                throw new InvalidMoveException();
            }
            base.Move(newSquare);
        }

        protected override bool CheckRules(ISquare newSquare)
        {

            var ranksToMove = Math.Abs((int)newSquare.Position.rank - (int)Square.Position.rank);
            var filesToMove = Math.Abs((int)newSquare.Position.file - (int)Square.Position.file);
            int totalMoves = filesToMove + ranksToMove;
            List<bool> rules = new List<bool>
            {
                totalMoves > 2
            };

            return rules.Any(r => r);
        }
    }
}