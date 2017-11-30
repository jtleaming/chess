using System.Collections.Generic;
using System.Linq;

namespace chess
{
    public static class Board
    {
        private static List<Square> squares = new List<Square>();
        public static List<Square> Squares
        {
            get { return squares; }
        }
        static Board()
        {
            for (int c = 1; c < 9; c++)
            {
                for (int i = 1; i < 9; i++)
                {
                    Squares.Add(new Square() { column = c.ConvertIntToLetter(), row = i });
                }
            }
        }
    }
}