using System;
using System.Collections.Generic;

namespace ChessEngine.Common
{
    public class RankBacker
    {
        public static RankBacker GetRank()
        {
            return new RankBacker();
        }
        private RankBacker()
        {}
        public readonly Lazy<List<char>> rank = new Lazy<List<char>>(() => new List<char> { '1', '2', '3', '4', '5', '6', '7', '8' });
        public char this[int index]
        {
            get
            {
                return rank.Value[index / 8];
            }
        }
        public char this[char index]
        {
            get
            {
                return rank.Value[(int)Char.GetNumericValue(index)-1];
            }
        }
    }
}