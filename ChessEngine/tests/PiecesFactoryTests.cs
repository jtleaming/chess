using System.Collections.Generic;
using System.Linq;
using ChessEngine.Factory;
using ChessEngine.Interfaces;
using ChessEngine.Pieces;
using ChessEngine.tests.Fixtures;
using FluentAssertions;
using Moq;
using Xunit;

namespace ChessEngine.tests
{
    public class PiecesFactoryTests
    {
        private PiecesFactory factory;
        private List<ISquare> squares;
        private Mock<IPlayer> mockPlayerOne;
        private Mock<IPlayer> mockPlayerTwo;
        private List<IPiece> playerOnePieces;
        private List<IPiece> playerTwoPieces;

        public PiecesFactoryTests()
        {
            factory = new PiecesFactory();
            squares = MockBoard.MockSquares.Select(d => d.Value).ToList();
            mockPlayerOne = new Mock<IPlayer>();
            mockPlayerTwo = new Mock<IPlayer>();

            playerOnePieces = factory.GetPlayerPieces(squares.GetRange(0,16), mockPlayerOne.Object);
            playerTwoPieces = factory.GetPlayerPieces(squares.GetRange(48, 16), mockPlayerTwo.Object);
        }

        [Fact]
        public void GetPlayerPieces_ForPlayerOne_ShouldReturn8PawnsOn2Rank()
        {
            playerOnePieces.Where(p => p.Position.rank == '2').Should().AllBeOfType<Pawn>();
            playerOnePieces.Where(p => p.Position.rank == '2').Count().Should().Be(8);
        }
        [Fact]
        public void GetPlayerPieces_ForPlayerTwo_ShouldReturn8PawnsOn7Rank()
        {
            playerTwoPieces.Where(p => p.Position.rank == '7') .Should().AllBeOfType<Pawn>();
        }
        [Fact]
        public void GetPlayerPieces_WhenFilesBandG_ShouldReturnKnightsForBothPlayers()
        {
            playerOnePieces.Where(p => p.Id == "b1" || p.Id == "g1").Should().AllBeOfType<Knight>();
            playerTwoPieces.Where(p => p.Id == "b8" || p.Id == "g8").Should().AllBeOfType<Knight>();
        }
        [Fact]
        public void GetPlayerPieces_WhenFilesAandH_ShouldReturnRooksForBothPlayers()
        {
            playerOnePieces.Where(p => p.Id == "a1" || p.Id == "h1").Should().AllBeOfType<Rook>();
            playerTwoPieces.Where(p => p.Id == "a8" || p.Id == "h8").Should().AllBeOfType<Rook>();
        }
        [Fact]
        public void GetPlayerPieces_WhenFilesCandF_ShouldReturnRooksForBothPlayers()
        {
            playerOnePieces.Where(p => p.Id == "c1" || p.Id == "f1").Should().AllBeOfType<Bishop>();
            playerTwoPieces.Where(p => p.Id == "c8" || p.Id == "f8").Should().AllBeOfType<Bishop>();
        }
        [Fact]
        public void GetPlayerPieces_WhenPlayerFileE_ShouldReturnKing()
        {
            playerOnePieces.First(p => p.Id == "e1").Should().BeOfType<King>();
            playerTwoPieces.First(p => p.Id == "e8").Should().BeOfType<King>();
        }
        [Fact]
        public void GetPlayerPieces_WhenPlayerFileD_ShouldReturnQueen()
        {
            playerOnePieces.First(p => p.Id == "d1").Should().BeOfType<Queen>();
            playerTwoPieces.First(p => p.Id == "d8").Should().BeOfType<Queen>();
        }
    }

}