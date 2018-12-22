using System;
using System.Collections.Generic;
using ChessEngine.Exceptions;
using ChessEngine.Interfaces;
using ChessEngine.Extensions;
using static ChessEngine.Common.Singletons;

namespace ChessEngine
{
    public class Square : ISquare
    {
        public (char file, char rank) Position { get; private set; }
        public string Id => Position.file.ToString()+Position.rank;
        public bool Occupied { get => Piece !=null; set{} }
        public IPiece Piece { get; set; }
        private string errorMessage = "{0} must be one of the following values: {1}";
        public Square(char file, char rank)
        {
            if(!Rank.Contains(rank))
            {
                throw new InvalidRankException(string.Format(errorMessage, rank, string.Join(",", Rank.rank.Value)));
            }
            if(!File.Contains(file))
            {
                throw new InvalidFileException(string.Format(errorMessage, "File", string.Join(",", File.file.Value)));
            }
            Position = (file,rank);
        }
    }

}