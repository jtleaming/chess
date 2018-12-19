using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using static ChessEngine.Common.Singletons;
using ChessEngine.Extensions;
using ChessEngine.Interfaces;

namespace ChessEngine
{
    public class Board : IBoard
    {
        public List<ISquare> Squares { get; } = new List<ISquare>();
        public Board()
        {
            var rankIndex = 0;
            var fileIndex = 0;
            for(int i = 0; i < 64; i++)
            {
                if(i%8 == 0 && i > 0)
                {
                    rankIndex++;
                    fileIndex = 0;
                }
                Squares.Add(new Square(Rank[rankIndex], File[fileIndex]));
                fileIndex++;
            }
        }
    }
}