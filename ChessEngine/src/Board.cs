using System.Collections.Generic;
using ChessEngine.Interfaces;
using static ChessEngine.Common.Singletons;

namespace ChessEngine
{
    public class Board : IBoard
    {
        public Dictionary<string, ISquare> Squares { get; } = new Dictionary<string, ISquare>();
        public Board()
        {
            for (int i = 0; i < 64; i++)
            {
                Squares.Add(File[i].ToString()+Rank[i], new Square(File[i], Rank[i]));
            }
        }
    }
} 