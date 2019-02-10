namespace ChessEngine.Interfaces
{
    public interface IPawn : IPiece
    {
        (bool canEnPassant, IPawn pieceToCapture, ISquare squareToMove) EnPassant { get; set; }
    }
}