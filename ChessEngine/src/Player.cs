using System.Collections.Generic;
using System.Linq;
using ChessEngine.Interfaces;

namespace ChessEngine
{
    public class Player : IPlayer
    {
        public List<IPiece> Pieces { get; } = new List<IPiece>();
        public bool Turn { get; set; }

        public List<IPiece> CapturedPieces { get; } = new List<IPiece>();

        public Player(Dictionary<string, ISquare> squares)
        {
            Pieces = squares.Select(s => s.Value.Piece = new Piece(s.Value, this)).ToList();
        }

    }
}