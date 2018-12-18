using System;
using System.Collections.Generic;
using ChessEngine.Exceptions;
using ChessEngine.Interfaces;

namespace ChessEngine
{
    internal class Square : ISquare
    {
        public (string rank, string file) Position { get; private set; }
        private List<string> rank = new List<string>{"a","b","c","d","e","f","g", "h"};
        private List<string> file = new List<string>{"1","2","3","4","5","6","7","8"};
        private string errorMessage = "{0} must be one of the following values: {1}";
        public Square(string rank, string file)
        {
            if(!this.rank.Contains(rank))
            {
                throw new InvalidRankException(string.Format(errorMessage, "Rank", string.Join(",", this.rank)));
            }
            if(!this.file.Contains(file))
            {
                throw new InvalidFileException(string.Format(errorMessage, "File", string.Join(",", this.file)));
            }
            Position = (rank,file);
        }
        public IPiece Piece { get; set; }
        public bool Occupied { get => Piece !=null; set{} }

    }

}