namespace ChessEngine.Interfaces
{
    public interface ISquare
    {
        (string rank, string file) Position { get; }
        string Id {get;}
        IPiece Piece { get; set; }
        bool Occupied { get; set; }
    }
}