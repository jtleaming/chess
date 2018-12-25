using System;
using System.Linq;
using System.Runtime.InteropServices;
using ChessEngine.Common;
using ChessEngine.Exceptions;
using ChessEngine.Interfaces;

namespace ChessEngine
{
    public abstract class Piece : IPiece
    {
        protected ISquare currentSquare;
        private readonly IPlayer player;

        public event EventHandler<TurnEventArgs> TurnHandler;

        public abstract IPlayer Player { get; }
        public virtual ISquare Square { get => currentSquare; set => value = currentSquare;}
        public (char file, char rank) Position { get => currentSquare.Position; }
        public string Id => currentSquare.Id;

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
        protected abstract bool CheckRules(ISquare newSquare);
        private void Capture(IPiece piece)
        {
            piece.Player.Pieces.Remove(piece);
            piece.Square = null;
            this.Player.CapturedPieces.Add(piece);
        }

    }
}