namespace ChessEngine.Interfaces
{
    public interface IPiece
    {
        ISquare Square { get; set; }
        IPlayer Player { get; }
        (string rank, string file) Position {get;}

        void Move(ISquare newSquare);
    }
}