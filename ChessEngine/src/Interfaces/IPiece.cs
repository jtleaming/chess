using System;

namespace ChessEngine.Interfaces
{
    public interface IPiece
    {
        ISquare Square { get; set; }
        IPlayer Player { get; }
        (string rank, string file) Position {get;}
        event EventHandler<MoveEventArgs> TakeTurn;

        void Move(ISquare newSquare);
    }
}