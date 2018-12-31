using System.Collections.Generic;

namespace ChessEngine.Interfaces
{
    public interface IBoard
    {
        Dictionary<string,ISquare> Squares { get; }

        void CheckForPiecesBetween(string pieceToMove, string locationToMove);
    }
}