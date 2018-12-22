using System.Collections.Generic;
using System.Linq;
using ChessEngine.Factory;
using ChessEngine.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;
using static ChessEngine.Common.Singletons;

namespace ChessEngine.tests
{
    public class PiecesFactoryTests
    {
        [Fact]
        public void GetPlayerPieces_ForPlayerOne_ShouldReturn8PawnsOn2Rank()
        {
            var factory = new PiecesFactory();
            var squares = new List<ISquare>();
            var mockPlayer = new Mock<IPlayer>();

            for (int i = 0; i < 16; i++)
            {
                var mockSquare = new Mock<ISquare>();
                mockSquare.Setup(s => s.Position).Returns((File[i], Rank[i]));
                mockSquare.SetupProperty(s => s.Piece);
                squares.Add(mockSquare.Object);
            };

            var pieces = factory.GetPlayerPieces(squares, mockPlayer.Object);
            pieces.Where(p => p.Position.rank == '2').Should().AllBeOfType<Pawn>();
            pieces.Where(p => p.Position.rank == '2').Count().Should().Be(8);
        }
        [Fact]
        public void GetPlayerPieces_ForPlayerTwo_ShouldReturn8PawnsOn7Rank()
        {
            var factory = new PiecesFactory();
            var squares = new List<ISquare>();
            var mockPlayer = new Mock<IPlayer>();

            for (int i = 47; i < 64; i++)
            {
                var mockSquare = new Mock<ISquare>();
                mockSquare.Setup(s => s.Position).Returns((file: File[i], rank: Rank[i]));
                mockSquare.SetupProperty(s => s.Piece);
                squares.Add(mockSquare.Object);
            };

            var pieces = factory.GetPlayerPieces(squares, mockPlayer.Object);
            pieces.Where(p => p.Position.rank == '7').Should().AllBeOfType<Pawn>();
        }
    }

}