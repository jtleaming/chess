using ChessEngine.Interfaces;

namespace ChessEngine
{
    public class Piece : IPiece
    {
        private ISquare currentSquare;
        public ISquare Square { get => currentSquare; set => value = currentSquare; }
        public (string rank, string file) Postion { get => currentSquare.Position; }

        public Piece(ISquare currentSquare)
        {
            this.currentSquare = currentSquare;
            currentSquare.Piece = this;
        }


        public void Move(ISquare newSquare)
        {
            currentSquare.Piece = null;
            currentSquare = newSquare;
            newSquare.Piece = this;
        }
    }
}