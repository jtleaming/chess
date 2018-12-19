using System;
using System.Collections.Generic;

namespace ChessEngine.Interfaces
{
    public interface IPlayer
    {
        List<IPiece> Pieces {get;}
        bool Turn { get; set; }
    }
}