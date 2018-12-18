using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ChessEngine.Interfaces;

namespace ChessEngine
{
    public class Board : IBoard
    {
        public List<ISquare> Squares { get; }
        private List<string> rank = new List<string>{"a","b","c","d","e","f","g", "h"};
        private List<string> file = new List<string>{"1","2","3","4","5","6","7","8"};
        public Board()
        {
            Squares = new List<ISquare>();
            var rank = 0;
            var file = 0;
            for(int i = 0; i < 64; i++)
            {
                if(i%8 == 0 && i > 0)
                {
                    rank++;
                    file = 0;
                }
                Squares.Add(new Square(this.rank[rank], this.file[file]));
                file++;
            }
        }
    }
}