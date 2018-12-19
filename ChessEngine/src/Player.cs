using System.Collections.Generic;
using System.Linq;
using ChessEngine.Interfaces;

namespace ChessEngine
{
    public class Player : IPlayer
    {
        public List<IPiece> Pieces { get; } = new List<IPiece>();
        public bool Turn { get; set;}

        public Player(List<ISquare> squares)
        {
            Pieces = squares.Select(s => s.Piece = new Piece(s, this)).ToList();
        }

    }
}