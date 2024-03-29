using System.Collections.Generic;

namespace ChessEngine.Interfaces
{
    public interface IPlayer
    {
        List<IPiece> Pieces {get;}
        List<IPiece> CapturedPieces { get; }
        bool Turn { get; set; }
        string IsPlayer { get; set; }

        void Move(string pieceToMove, string locationToMove);
    }
}