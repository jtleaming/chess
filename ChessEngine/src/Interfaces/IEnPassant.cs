using System.Collections.Generic;

namespace ChessEngine.Interfaces
{
    public interface IEnPassant
    {
         Dictionary<string,ISquare> Squares { get; set;}
         void CheckEnPassant(IPawn pawn, ISquare squaresToMove);
    }
}