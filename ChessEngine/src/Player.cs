using System;
using System.Collections.Generic;
using System.Linq;
using ChessEngine.Common;
using ChessEngine.Exceptions;
using ChessEngine.Interfaces;
using ChessEngine.Pieces;

namespace ChessEngine
{
    public class Player : IPlayer
    {
        private readonly IBoard board;
        private readonly ILeaps leaps;

        public List<IPiece> Pieces { get; } = new List<IPiece>();
        public bool Turn { get; internal set; }

        public List<IPiece> CapturedPieces { get; } = new List<IPiece>();
        public string IsPlayer { get; set; }

        public Player(List<ISquare> squares, Func<List<ISquare>, IPlayer, List<IPiece>> pieces, IBoard board, ILeaps leaps)
        {
            Pieces = pieces(squares, this);
            this.board = board;
            this.leaps = leaps;
        }

        public void Move(string move)
        {
            string[] moves = move.Split(' ');
            if (move.ToLower().Contains("o"))
            {
                Castle(moves);
            }
            else
            {
                string pieceToMove = moves[0].ToString();
                string locationToMove = moves[1].ToString();

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

        private void Castle(string[] move)
        {
            try
            {
                bool playerOne = IsPlayer == "One";
                ISquare kingMoveSquare = null;
                ISquare rookMoveSquare = null;
                King king = (King)Pieces.FirstOrDefault(p => p.Id == (playerOne ? "e1" : "e8"));
                IPiece rook = null;
                if (move.Count() == 2)
                {
                    kingMoveSquare = board.Squares[(playerOne ? "g1" : "g8")];
                    rookMoveSquare = board.Squares[(playerOne ? "f1" : "f8")];
                    rook = Pieces.FirstOrDefault(p => p.Id == (playerOne ? "h1" : "h8"));
                }
                else if (move.Count() == 3)
                {
                    kingMoveSquare = board.Squares[(playerOne ? "c1" : "c8")];
                    rookMoveSquare = board.Squares[(playerOne ? "d1" : "d8")];
                    rook = Pieces.FirstOrDefault(p => p.Id == (playerOne ? "a1" : "a8"));
                }
                else
                {
                    throw new InvalidMoveException();
                }

                if (king.FirstMove && rook.FirstMove)
                {
                    leaps.CheckForPiecesBetween(king.Id, kingMoveSquare.Id);
                    king.Castling = (rookMoveSquare, rook);
                    king.Move(kingMoveSquare);
                }
            }
            catch (InvalidMoveException)
            {
                throw;
            }
        }
    }
}