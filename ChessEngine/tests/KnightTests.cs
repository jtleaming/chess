using System.Collections.Generic;
using ChessEngine.Interfaces;
using ChessEngine.Pieces;
using Moq;
using Xunit;

namespace ChessEngine.tests
{
    public class KnightTests
    {
        private Mock<IPlayer> mockPlayer;
        private Mock<ISquare> mockSquare;

        public KnightTests()
        {
            mockPlayer = new Mock<IPlayer>();
            mockSquare = new Mock<ISquare>();
        }

        [Fact]
        public void GetValidMoves_WhenKnightOnB1_ShouldReturnAllValidMoves()
        {
            var knight = new Knight(mockSquare.Object, mockPlayer.Object);

            List<(char file, char rank)> validMoves = knight.GetValidMoves();
        }
    }
}