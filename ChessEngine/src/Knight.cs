using System;
using System.Collections.Generic;
using ChessEngine.Interfaces;

namespace ChessEngine
{
    public class Knight : Piece
    {
        public Knight(ISquare currentSquare, IPlayer player) : base(currentSquare, player)
        {
        }

        public override IPlayer Player => throw new NotImplementedException();

        public override ISquare Square { get => throw new NotImplementedException();}

        public List<(char file, char rank)> GetValidMoves()
        {
            throw new NotImplementedException();
        }

        protected override bool CheckRules(ISquare newSquare)
        {
            throw new NotImplementedException();
        }
    }
}