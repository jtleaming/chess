using System;
using ChessEngine.Common;

namespace ChessEngine.Interfaces
{
    public interface IPiece
    {
        ISquare Square { get; set; }
        IPlayer Player { get; }
        (char file, char rank) Position {get;}
        event EventHandler<TurnEventArgs> TurnHandler;
        void Move(ISquare newSquare);
    }
}