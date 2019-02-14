using System.Collections.Generic;
using ChessEngine.Interfaces;
using static ChessEngine.Common.Singletons;
using Moq;

namespace ChessEngine.tests.Fixtures
{
    public class MockBoard
    {
        private static int BoardSize { get; set; } = 64;
        public static Dictionary<string, ISquare> MockSquares
        {
            get
            {
                Dictionary<string, ISquare> squares = new Dictionary<string, ISquare>();
                for (int i = 0; i < BoardSize; i++)
                {
                    var mockSquare = new Mock<ISquare>();
                    mockSquare.Setup(s => s.Position).Returns((File[i], Rank[i]));
                    mockSquare.Setup(s => s.Id).Returns(File[i].ToString() + Rank[i]);
                    mockSquare.SetupProperty(s => s.Piece);
                    squares.Add(File[i].ToString() + Rank[i], mockSquare.Object);
                };

                return squares;
            }
        }
        public static Dictionary<string, ISquare> GetBoardOfSizeN(int boardSize)
        {
            BoardSize = boardSize;
            return MockSquares;
        }
    }
}