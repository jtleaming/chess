namespace ChessEngine.Interfaces
{
    public interface ISquare
    {
        (char file, char rank) Position { get; }
        string Id {get;}
        IPiece Piece { get; set; }
        bool Occupied { get; set; }
    }
}