using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ChessEngine.Interfaces;
using ChessEngine.Extensions;
using FluentAssertions;
using Moq;
using Xunit;
using ChessEngine.Exceptions;
using static ChessEngine.Common.Singletons;
using ChessEngine.Common;
using System;

namespace ChessEngine.tests
{
    public class PlayerTests
    {
        private List<Mock<ISquare>> mockSquares;
        private Mock<ISquare> mockSquare;
        private IPlayer player;
        public PlayerTests()
        {
            mockSquares = new List<Mock<ISquare>>();
            mockSquare = new Mock<ISquare>();
            mockSquares.AddMultiple(16, mockSquare);
        }
        private List<IPiece> mockFactory(List<ISquare> squares, IPlayer player)
        {
            List<IPiece> pieces = new List<IPiece>();
            squares.ForEach(s =>
            {
                var mockPiece = new Mock<IPiece>();
                mockPiece.Setup(p => p.Position).Returns(s.Position);
                pieces.Add(mockPiece.Object);
            });
            return pieces;
        }
        private IPlayer getPlayer() => new Player(mockSquares.Select(ms => ms.Object).ToList(), mockFactory);
        private void MockTurnListener(object sender, TurnEventArgs e)
        {
        }

        [Fact]
        public void Player_WhenCreated_ShouldHave16Pieces()
        {
            player = getPlayer();
            player.Pieces.Count.Should().Be(16);
        }
        [Fact]
        public void Player_FirstPiece_ShouldSquareA1()
        {
            mockSquare.SetupGet(a => a.Position).Returns(('a', '1'));
            player = getPlayer();
            player.Pieces.First().Position.Should().Be(('a', '1'));
        }
        [Fact]
        public void Player_LastPiece_ShouldSquareH2()
        {
            mockSquare.Setup(a => a.Position).Returns(('h', '2'));
            player = getPlayer();
            player.Pieces.Last().Position.Should().Be(('h', '2'));
        }
        [Fact]
        public void Move_WhenGivenPieceQuardenantsAndNewSquare_ShouldMovePieceToNewSquare()
        {
            // var firstRow = new KeyValuePair<char,List<char>>('1', File);
            // var secondRow = new KeyValuePair<char, List<char>>('2', File);
            // var rowIndex = 0;
            // mockSquares.ForEach(ms => 
            //         {
            //             var row = rowIndex < 7 ? firstRow : secondRow;
            //             ms.Setup(s => s.Position).Returns((row.Value[rowIndex], row.Key));
            //             rowIndex++;
            //         });
        }
    }
}