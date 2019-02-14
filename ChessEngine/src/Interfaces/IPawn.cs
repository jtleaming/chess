namespace ChessEngine.Interfaces
{
    public interface IPawn : IPiece
    {
        (IPawn pieceToCapture, ISquare squareToMove) EnPassant { get; set; }
    }
}