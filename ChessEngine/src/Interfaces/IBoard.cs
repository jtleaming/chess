using System.Collections.Generic;

namespace ChessEngine.Interfaces
{
    public interface IBoard
    {
        List<ISquare> Squares { get; }
    }
}