namespace ChessEngine.Interfaces
{
    public interface IPiece
    {
        ISquare Square { get; set; }

        void Move(ISquare newSquare);
    }
}