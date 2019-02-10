using System;
using System.Collections.Generic;
using ChessEngine.Common;
using ChessEngine.Exceptions;
using ChessEngine.Interfaces;
using ChessEngine.Pieces;
using FluentAssertions;
using Moq;
using Xunit;

namespace ChessEngine.tests
{
    public class PawnTests
    {
        private Mock<ISquare> mockCurrentSquare;
        private Mock<IPlayer> mockPlayer;
        private Mock<ISquare> mockNewSquare;
        private Mock<IPiece> mockNewPiece;
        private Mock<IPlayer> mockOtherPlayer;
        private Pawn pawn;

        public PawnTests()
        {
            mockCurrentSquare = new Mock<ISquare>();
            mockPlayer = new Mock<IPlayer>();
            mockNewSquare = new Mock<ISquare>();
            mockNewPiece = new Mock<IPiece>();
            mockOtherPlayer = new Mock<IPlayer>();

            mockCurrentSquare.Setup(s => s.Position).Returns(('b', '2'));
            mockPlayer.Setup(p => p.IsPlayer).Returns("One");
            mockPlayer.Setup(p => p.CapturedPieces).Returns(new List<IPiece>());

            mockOtherPlayer.Setup(pl => pl.Pieces).Returns(new List<IPiece> { mockNewPiece.Object });

            pawn = new Pawn(mockCurrentSquare.Object, mockPlayer.Object);
            pawn.TurnHandler += MockTurnEventListener;
        }

        private void MockTurnEventListener(object sender, TurnEventArgs e)
        {
        }

        [Fact]
        public void Pawn_WhenMoveFile_ShouldThrowInvalidMoveException()
        {
            mockNewSquare.Setup(s => s.Position).Returns(('a', '2'));
            mockPlayer.SetupProperty(p => p.Turn, true);

            Assert.Throws<InvalidMoveException>(() => pawn.Move(mockNewSquare.Object));
        }
        [Fact]
        public void Pawn_WhenMoveRankBack_ShouldThrowInvalidMoveException()
        {
            mockNewSquare.Setup(s => s.Position).Returns(('b', '1'));
            mockPlayer.SetupProperty(p => p.Turn, true);

            Assert.Throws<InvalidMoveException>(() => pawn.Move(mockNewSquare.Object));
        }
        [Fact]
        public void Pawn_WhenFirstMoveFalse_MoveTwoFileShouldThrowInvalidMoveException()
        {
            mockNewSquare.Setup(s => s.Position).Returns(('b', '3'));
            mockPlayer.SetupProperty(p => p.Turn, true);

            pawn.Move(mockNewSquare.Object);

            var newSquare = new Mock<ISquare>();
            newSquare.Setup(s => s.Position).Returns(('b', '5'));
            mockPlayer.SetupProperty(p => p.Turn, true);

            Assert.Throws<InvalidMoveException>(() => pawn.Move(newSquare.Object));
        }
        [Fact]
        public void Pawn_WhenFirstMoveTrue_MoveGreaterThanTwoFileShouldThrowInvalidMoveException()
        {
            mockNewSquare.Setup(s => s.Position).Returns(('b', '5'));
            mockPlayer.SetupProperty(p => p.Turn, true);

            Assert.Throws<InvalidMoveException>(() => pawn.Move(mockNewSquare.Object));
        }
        [Fact]
        public void Pawn_WhenFirstMoveTrue_MoveTwoIsValidMove()
        {
            mockNewSquare.Setup(s => s.Position).Returns(('b', '4'));
            mockPlayer.SetupProperty(p => p.Turn, true);
            var move = Record.Exception(() => pawn.Move(mockNewSquare.Object));

            Assert.Null(move);
        }
        [Fact]
        public void Pawn_WhenMoveOneFileDiagonallyOccupiedByOpponent_ShouldBeValid()
        {
            mockNewSquare.Setup(s => s.Position).Returns(('a', '3'));

            mockNewSquare.Setup(s => s.Occupied).Returns(true);
            mockNewSquare.Setup(s => s.Piece).Returns(mockNewPiece.Object);

            mockNewPiece.Setup(p => p.Player).Returns(mockOtherPlayer.Object);

            mockPlayer.SetupProperty(p => p.Turn, true);
            pawn.Move(mockNewSquare.Object);
        }

        [Fact]
        public void Pawn_WhenMoveTwoFileDiagonallyOccupiedByOpponent_ShouldThrowException()
        {
            mockNewSquare.Setup(s => s.Position).Returns(('d', '3'));

            mockNewSquare.Setup(s => s.Occupied).Returns(true);
            mockNewSquare.Setup(s => s.Piece).Returns(mockNewPiece.Object);

            mockNewPiece.Setup(p => p.Player).Returns(mockOtherPlayer.Object);

            mockPlayer.SetupProperty(p => p.Turn, true);
            Assert.Throws<InvalidMoveException>(() => pawn.Move(mockNewSquare.Object));
        }

        [Fact]
        public void Pawn_WhenMovesToEnPassantSquare_CapturesEnPassantPiece()
        {
            mockPlayer.Setup(p => p.Turn).Returns(true);

            mockNewSquare.SetupProperty(p => p.Piece);
            mockNewSquare.Setup(s => s.Position).Returns(('c', '3'));
            mockNewSquare.Setup(s => s.Id).Returns(("c3"));
            mockNewSquare.Setup(s => s.Occupied).Returns(true);

            Mock<IPawn> mockOtherPawn = new Mock<IPawn>();
            Mock<IPlayer> mockOtherPlayer = new Mock<IPlayer>();

            mockOtherPlayer.Setup(pl => pl.Pieces).Returns(
               new List<IPiece> { mockOtherPawn.Object }
            );

            mockOtherPawn.Setup(p => p.Position).Returns(('c', '2'));
            mockOtherPawn.Setup(pa => pa.Player).Returns(mockOtherPlayer.Object);

            pawn.EnPassant = (mockOtherPawn.Object, mockNewSquare.Object);

            pawn.Move(mockNewSquare.Object);

            pawn.Player.CapturedPieces.Should().Contain(mockOtherPawn.Object);
        }

    }
}