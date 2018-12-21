using System;
using System.Collections.Generic;
using ChessEngine.Interfaces;

namespace ChessEngine.Factory
{
    public class PiecesFactory
    {
        public List<IPiece> GetPlayerPieces(List<ISquare> squares)
        {
            List<IPiece> pieces = new List<IPiece>();
            squares.ForEach(s => 
                {
                    if(s.Position.rank == 2){
                        
                    }
                }
            )
            return pieces;
        }
    }
}