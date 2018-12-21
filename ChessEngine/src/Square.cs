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
        public string Id => Position.rank+Position.file;
        public bool Occupied { get => Piece !=null; set{} }
        public IPiece Piece { get; set; }
        private string errorMessage = "{0} must be one of the following values: {1}";
        public Square(string file, string rank)
        {
            if(!Rank.Contains(rank))
            {
                throw new InvalidRankException(string.Format(errorMessage, "Rank", string.Join(",", Rank)));
            }
            if(!File.Contains(file))
            {
                throw new InvalidFileException(string.Format(errorMessage, "File", string.Join(",", File)));
            }
            Position = (file,rank);
        }
    }

}