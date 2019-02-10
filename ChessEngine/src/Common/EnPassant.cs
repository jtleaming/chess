using System.Collections.Generic;
using ChessEngine.Interfaces;

namespace ChessEngine.Common
{
    public class EnPassant
    {
        public Dictionary<string, ISquare> Squares;
        public EnPassant()
        {
        }

        public void CheckEnPassant(IPawn pawn, ISquare squaresToMove)
        {
            var firstAdjacentSquare = Squares[char.ConvertFromUtf32(squaresToMove.Position.file - 1) + char.GetNumericValue(squaresToMove.Position.rank)];
            var secondAdjacentSquare = Squares[char.ConvertFromUtf32(squaresToMove.Position.file + 1) + char.GetNumericValue(squaresToMove.Position.rank)];

            bool firstCondition = firstAdjacentSquare.Occupied && firstAdjacentSquare.Piece is IPawn && firstAdjacentSquare.Piece.Player != pawn.Player;
            bool secondCondition = secondAdjacentSquare.Occupied && secondAdjacentSquare.Piece is IPawn && secondAdjacentSquare.Piece.Player != pawn.Player;

            if (firstCondition || secondCondition)
            {
                IPawn adjacentPawn = firstCondition ? firstAdjacentSquare.Piece as IPawn : secondAdjacentSquare.Piece as IPawn;
                ISquare square = pawn.Player.IsPlayer == "Two" ?
                Squares[squaresToMove.Position.file.ToString() + (char.GetNumericValue(squaresToMove.Position.rank) + 1)] :
                Squares[squaresToMove.Position.file.ToString() + (char.GetNumericValue(squaresToMove.Position.rank) - 1)];
                adjacentPawn.EnPassant = (pawn, square);
            }

        }
    }
}