using System;

namespace chess
{
    public class Square
    {
        public char column;
        public int row;
        public Square(string square)
        {
            var convertedSquare = square.ToCharArray();

            column = convertedSquare[0];
            row = Int32.Parse(convertedSquare[1].ToString());
        }
        public Square()
        {
        }
    }
}