using System;
using System.Linq;
using System.Runtime.InteropServices;
using ChessEngine.Common;
using ChessEngine.Exceptions;
using ChessEngine.Interfaces;
using Microsoft.Win32.SafeHandles;

namespace ChessEngine
{
    public class Piece : IPiece
    {
        private ISquare currentSquare;
        private readonly IPlayer player;

        public event EventHandler<TurnEventArgs> TurnHandler;

        public IPlayer Player { get => player; }
        public virtual ISquare Square { get => currentSquare; set => value = currentSquare; }
        public (string rank, string file) Position { get => currentSquare.Position; }

        public Piece(ISquare currentSquare, IPlayer player)
        {
            this.currentSquare = currentSquare;
            this.player = player;
            currentSquare.Piece = this;
        }

        public virtual void Move(ISquare newSquare)
        {
            if (!player.Turn)
            {
                throw new InvalidMoveException("It is not the players turn");
            }

            if (newSquare.Occupied && newSquare.Piece.Player == this.player)
            {
                throw new InvalidMoveException("Square occupied by current player");
            }

            if (newSquare.Occupied && newSquare.Piece.Player != this.player)
            {
                Capture(newSquare.Piece);
            }

            currentSquare.Piece = null;
            currentSquare = newSquare;
            newSquare.Piece = this;

            TurnHandler.Invoke(this, new TurnEventArgs(player));
        }

        private void Capture(IPiece piece)
        {
            piece.Player.Pieces.Remove(piece);
            piece.Square = null;
            this.Player.CapturedPieces.Add(piece);
        }

    }
}