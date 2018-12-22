using System;
using System.Collections.Generic;

namespace ChessEngine.Common
{
    public sealed class Singletons
    {
        private Singletons(){}
        private static Rank rank = null;
        private static File file = null;
        public static Rank Rank => rank = rank == null ? new Rank() : rank;
        public static File File => file = file == null ? new File() : file;
    }

    public class File
    {
        public readonly Lazy<List<char>> file = new Lazy<List<char>>(() => new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' });
        public char this[int index]
        {
            get
            {
                return file.Value[index % 8];
            }
        }
        public char this[char index]
        {
            get 
            {
                var intIndex = Convert.ToInt32(index) - 97;
                return file.Value[intIndex];
            }
        }
    }
    public class Rank
    {
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