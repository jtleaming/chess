using System;
using System.Collections.Generic;
using ChessEngine.Extensions;
using ChessEngine.Interfaces;

namespace ChessEngine.Factory
{
    public class PiecesFactory
    {
        public List<IPiece> GetPlayerPieces(List<ISquare> squares, IPlayer player)
        {
            List<IPiece> pieces = new List<IPiece>();
            squares.ForEach(s =>
                {
                    if (s.Position.rank == '2' || s.Position.rank == '7')
                    {
                        s.Piece = new Pawn(s, player);
                    }
                    else
                    {
                        s.Piece = new Piece(s, player);
                    }
                    pieces.Add(s.Piece);
                }
            );
            return pieces;
        }

    }
}