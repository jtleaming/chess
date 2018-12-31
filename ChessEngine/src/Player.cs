using System;
using System.Collections.Generic;
using System.Linq;
using ChessEngine.Exceptions;
using ChessEngine.Interfaces;

namespace ChessEngine
{
    public class Player : IPlayer
    {
        private readonly IBoard board;

        public List<IPiece> Pieces { get;} = new List<IPiece>();
        public bool Turn { get; set; }

        public List<IPiece> CapturedPieces { get; } = new List<IPiece>();
        public string IsPlayer { get; set;}

        public Player(List<ISquare> squares,Func<List<ISquare>, IPlayer, List<IPiece>> pieces, IBoard board)
        {
            Pieces = pieces(squares, this);
            this.board = board;
        }

        public void Move(string pieceToMove, string locationToMove)
        {
            try
            {
                var piece = Pieces.FirstOrDefault(p => p.Id == pieceToMove);
                piece.Move(board.Squares[locationToMove]);
            }
            catch (NullReferenceException)
            {
                throw new InvalidMoveException($"Player does not have a piece on {locationToMove}");
            }
        }
    }
}