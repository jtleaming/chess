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
using ChessEngine.Pieces;

namespace ChessEngine.tests
{
    public class PlayerTests
    {
        private List<Mock<ISquare>> mockSquares;
        private IPlayer player = null;
        private Mock<IBoard> mockBoard;
        private List<Mock<IPiece>> mockPieces = new List<Mock<IPiece>>();
        private Mock<ILeaps> leaps = new Mock<ILeaps>(MockBehavior.Strict);


        private IPlayer Player => player = player == null ? new Player(mockSquares.Select(ms => ms.Object).ToList(), mockFactory, mockBoard.Object, leaps.Object) : player;
        public PlayerTests()
        {
            mockSquares = new List<Mock<ISquare>>();
            for (int i = 0; i < 32; i++)
            {
                mockSquares.Add(new Mock<ISquare>());
            }
            mockBoard = new Mock<IBoard>();
        }
        private List<IPiece> mockFactory(List<ISquare> squares, IPlayer player)
        {
            List<IPiece> pieces = new List<IPiece>();
            var rowIndex = 0;
            mockSquares.ForEach(ms =>
                    {
                        var rankAndFile = (File[rowIndex], Rank[rowIndex]);
                        ms.Setup(s => s.Position).Returns(rankAndFile);
                        ms.Setup(s => s.Id).Returns(rankAndFile.Item1.ToString() + rankAndFile.Item2);

                        if (rowIndex < 16)
                        {
                            var mockPiece = new Mock<IPiece>();
                            mockPiece.Setup(p => p.Id).Returns(ms.Object.Id);
                            mockPiece.Setup(p => p.Position).Returns(rankAndFile);
                            ms.Setup(s => s.Occupied).Returns(true);
                            pieces.Add(mockPiece.Object);
                            mockPieces.Add(mockPiece);
                        }

                        rowIndex++;
                    });
            mockBoard.Setup(b => b.Squares).Returns(new Dictionary<string, ISquare>(mockSquares.Select(s => new KeyValuePair<string, ISquare>(s.Object.Id, s.Object))));
            return pieces;
        }
        private void MockTurnListener(object sender, TurnEventArgs e)
        {
        }

        [Fact]
        public void Player_WhenCreated_ShouldHave16Pieces()
        {
            Player.Pieces.Count.Should().Be(16);
        }
        [Fact]
        public void Player_FirstPiece_ShouldSquareA1()
        {
            Player.Pieces.First().Position.Should().Be(('a', '1'));
        }
        [Fact]
        public void Player_LastPiece_ShouldSquareH2()
        {
            Player.Pieces.Last().Position.Should().Be(('h', '2'));
        }
        [Fact]
        public void Move_WhenGivenPieceQuardenantsAndNewSquare_ShouldCallPieceMove()
        {
            leaps.Setup(l => l.CheckForPiecesBetween(It.IsAny<string>(), It.IsAny<string>()));
            Player.Move("b2", "b3");
            mockPieces.FirstOrDefault(p => p.Object.Id == "b2").Verify(p => p.Move(It.IsAny<ISquare>()));
        }
        [Fact]
        public void Move_WhenGivenPieceQuardenantsWithoutPlayerPiece_ShouldThrowInvalidMoveException()
        {
            Assert.Throws<InvalidMoveException>(() => Player.Move("b3", "b4"));
        }
        [Fact]
        public void Move_WhenPlayerPieceIsKnightAndLeapsOverPlayerPiece_ShouldNotCallCheckForPiecesBetween()
        {
            Player.Pieces.Remove(Player.Pieces.First(p => p.Id == "b1"));
            Mock<IPiece> mockKnight = new Mock<IPiece>();
            mockKnight.Setup(k => k.Id).Returns("b1");
            mockKnight.Setup(k => k.GetType()).Returns(typeof(Knight));
            Player.Pieces.Add(mockKnight.Object);

            Player.Move("b1", "b4");
        }
    }
}