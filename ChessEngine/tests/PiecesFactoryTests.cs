using System.Collections.Generic;
using ChessEngine.Factory;
using ChessEngine.Interfaces;
using Moq;
using Xunit;
using static ChessEngine.Common.Singletons;

namespace ChessEngine.tests
{
    public class PiecesFactoryTests
    {
        [Fact]
        public void GetPlayerPieces_ForPlayerOne_ShouldReturn8PawnsOnBRank()
        {
            var factory = new PiecesFactory();
            var squares = new List<ISquare>();

            for (int i = 0; i < 16; i++)
            {
                var file = i<8 ? 0 : 1;
                var rank = i >= 8 ? i-8 : i;
                var mockSquare = new Mock<ISquare>();
                mockSquare.Setup(s => s.Position).Returns((File[file],Rank[rank]));
                squares.Add(mockSquare.Object);
            };

            var pieces = factory.GetPlayerPieces(squares);
            
        }
    }

}