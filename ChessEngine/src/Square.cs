using System;
using System.Collections.Generic;
using ChessEngine.Exceptions;
using ChessEngine.Interfaces;
using static ChessEngine.Common.Singletons;

namespace ChessEngine
{
    public class Square : ISquare
    {
        public (string rank, string file) Position { get; private set; }
        private string errorMessage = "{0} must be one of the following values: {1}";
        public Square(string rank, string file)
        {
            if(!Rank.Contains(rank))
            {
                throw new InvalidRankException(string.Format(errorMessage, "Rank", string.Join(",", Rank)));
            }
            if(!File.Contains(file))
            {
                throw new InvalidFileException(string.Format(errorMessage, "File", string.Join(",", File)));
            }
            Position = (rank,file);
        }
        public IPiece Piece { get; set; }
        public bool Occupied { get => Piece !=null; set{} }

    }

}