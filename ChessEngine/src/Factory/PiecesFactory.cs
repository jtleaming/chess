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
                        if (s.Position.file == 'b' || s.Position.file == 'g')
                        {
                            s.Piece = new Knight(s, player);
                        }
                        else if(s.Position.file == 'a' || s.Position.file == 'h')
                        {
                            s.Piece = new Rook(s, player);
                        }
                        else if(s.Position.file == 'c' || s.Position.file == 'f')
                        {
                            s.Piece = new Bishop(s, player);
                        }
                        else if(s.Id == "d1" || s.Id == "e8")
                        {
                            s.Piece = new King(s, player);
                        }
                        else
                        {
                            s.Piece = new Queen(s, player);
                        }
                    }
                    pieces.Add(s.Piece);
                }
            );
            return pieces;
        }

    }
}