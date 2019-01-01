using System;
using System.Collections.Generic;
using ChessEngine.Extensions;
using ChessEngine.Interfaces;
using ChessEngine.Pieces;

namespace ChessEngine.Factory
{
    public class PiecesFactory
    {
        public List<IPiece> GetPlayerPieces(List<ISquare> squares, IPlayer player)
        {
            List<IPiece> pieces = new List<IPiece>();
            squares.ForEach(s =>
                {
                    switch (s.Position)
                    {
                        case var position when position.rank == '2' || position.rank == '7':
                            s.Piece = new Pawn(s, player);
                            break;
                        case var position when position.file == 'b' || position.file == 'g':
                            s.Piece = new Knight(s, player);
                            break;
                        case var position when position.file == 'a' || position.file == 'h':
                            s.Piece = new Rook(s, player);
                            break;
                        case var position when position.file == 'c' || position.file == 'f':
                            s.Piece = new Bishop(s, player);
                            break;
                        case var position when position.file == 'e':
                            s.Piece = new King(s, player);
                            break;
                        default:
                            s.Piece = new Queen(s, player);
                            break;
                    }
                    pieces.Add(s.Piece);
                }
            );
            return pieces;
        }

    }
}