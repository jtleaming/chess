using System;
using System.Collections.Generic;
using System.Linq;
using ChessEngine.Exceptions;
using ChessEngine.Interfaces;

namespace ChessEngine.Pieces
{
    public class Knight : Piece
    {
        private readonly IPlayer player;

        public override IPlayer Player => player;
        public override ISquare Square { get => base.currentSquare; }
        public Knight(ISquare currentSquare, IPlayer player) : base(currentSquare, player)
        {
            this.player = player;
        }

        public override void Move(ISquare newSquare)
        {
            if(CheckRules(newSquare))
            {
                throw new InvalidMoveException("Knight cannot make this move.");
            }
            base.Move(newSquare);
        }

        public List<(char file, char rank)> GetValidMoves()
        {
            List<(char,char)> validMoves = new List<(char,char)>();
            return validMoves;
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