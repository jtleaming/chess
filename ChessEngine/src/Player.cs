using System;
using System.Collections.Generic;
using System.Linq;
using ChessEngine.Common;
using ChessEngine.Exceptions;
using ChessEngine.Interfaces;
using Newtonsoft.Json;

namespace ChessEngine
{
    public class Player : IPlayer
    {
        private readonly IBoard board;
        private readonly ILeaps leaps;
        public List<IPiece> Pieces { get; } = new List<IPiece>();
        public bool Turn { get; set; }

        public List<IPiece> CapturedPieces { get; } = new List<IPiece>();
        public string IsPlayer { get; set; }

        public Player(List<ISquare> squares, Func<List<ISquare>, IPlayer, List<IPiece>> pieces, IBoard board, ILeaps leaps)
        {
            Pieces = pieces(squares, this);
            this.board = board;
            this.leaps = leaps;
        }

        public void Move(string pieceToMove, string locationToMove)
        {
            try
            {
                var piece = Pieces.FirstOrDefault(p => p.Id == pieceToMove);

                if (!(piece.GetType() == typeof(Pieces.Knight)))
                {
                    leaps.CheckForPiecesBetween(pieceToMove, locationToMove);
                }

                piece.Move(board.Squares[locationToMove]);
            }
            catch (NullReferenceException)
            {
                throw new InvalidMoveException($"Player does not have a piece on {locationToMove}");
            }
        }
    }
}