using System;
using System.Collections.Generic;

namespace ChessEngine.Common
{
    public class FileBacker
    {
        private FileBacker()
        {
        }
        public static FileBacker GetFile()
        {
            return new FileBacker();
        }
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
}