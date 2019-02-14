using ChessEngine.Common;
using ChessEngine.Exceptions;
using ChessEngine.Interfaces;
using ChessEngine.tests.Fixtures;
using Moq;
using Xunit;

namespace ChessEngine.tests
{
    public class LeapsTests
    {
        private Leaps leaps;
        private readonly Mock<IBoard> board;

        public LeapsTests()
        {
            board = new Mock<IBoard>() {DefaultValue = DefaultValue.Mock};
            board.Setup(b => b.Squares).Returns(MockBoard.MockSquares);
            leaps = new Leaps(board.Object);
        }

        [Fact]
        public void CheckForPiecesBetween_WhenGivenHorizontalLocations_ShouldThrowInvalidMoveExceptionIfAnySquaresBetweenAreOccupied()
        {
            var mockSquare = Mock.Get(board.Object.Squares["c1"]);
            mockSquare.Setup(s => s.Occupied).Returns(true);
            leaps.Squares["c1"].Piece = new Mock<IPiece>().Object;
            Assert.Throws<InvalidMoveException>(() => leaps.CheckForPiecesBetween("a1", "d1"));
            Assert.Throws<InvalidMoveException>(() => leaps.CheckForPiecesBetween("d1", "a1"));
        }
        [Fact]
        public void CheckForPiecesBetween_WhenGivenDiagonalLocations_ShouldThrowInvalidMoveExceptionIfAnySquaresBetweenAreOccupied()
        {
            leaps.Squares["c3"].Piece = new Mock<IPiece>().Object;
            var mockSquare = Mock.Get(board.Object.Squares["c3"]);
            mockSquare.Setup(s => s.Occupied).Returns(true);
            Assert.Throws<InvalidMoveException>(() => leaps.CheckForPiecesBetween("a1", "e5"));
            Assert.Throws<InvalidMoveException>(() => leaps.CheckForPiecesBetween("e5", "a1"));

            leaps.Squares["g3"].Piece = new Mock<IPiece>().Object;
            mockSquare = Mock.Get(board.Object.Squares["g3"]);
            mockSquare.Setup(s => s.Occupied).Returns(true);
            Assert.Throws<InvalidMoveException>(() => leaps.CheckForPiecesBetween("e5", "h2"));
            Assert.Throws<InvalidMoveException>(() => leaps.CheckForPiecesBetween("h2", "e5"));
        }
        [Fact]
        public void CheckForPiecesBetween_WhenGivenVerticalLocations_ShouldThrowInvalidMoveExceptionIfAnySquaresBetweenAreOccupied()
        {
            leaps.Squares["a3"].Piece = new Mock<IPiece>().Object;
            var mockSquare = Mock.Get(board.Object.Squares["a3"]);
            mockSquare.Setup(s => s.Occupied).Returns(true);
            Assert.Throws<InvalidMoveException>(() => leaps.CheckForPiecesBetween("a1", "a5"));
            Assert.Throws<InvalidMoveException>(() => leaps.CheckForPiecesBetween("a5", "a1"));
        }
    }
}