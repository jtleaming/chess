using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ChessEngine.Interfaces;
using Moq;
using Xunit;

namespace ChessEngine.tests
{
    public class RulesEngineTests
    {
        [Fact]
        public void TestName()
        {
            var mockPiece = new Mock<IPiece>();
            var currentSquare = new Mock<ISquare>();

            currentSquare.Setup(s => s.Position).Returns(('a','2'));
            currentSquare.Setup(s => s.Id).Returns("a2");
            currentSquare.Setup(s => s.Occupied).Returns(true);
            currentSquare.Setup(s => s.Piece).Returns(mockPiece.Object);

            mockPiece.Setup(p => p.Square).Returns(currentSquare.Object);
            mockPiece.Setup(p => p.Position).Returns(currentSquare.Object.Position);
            mockPiece.Setup(p => p.Id).Returns(currentSquare.Object.Id);

        }
    }

}