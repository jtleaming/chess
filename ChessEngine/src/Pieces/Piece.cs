using System;
using System.Text.Json.Serialization;
using ChessEngine.Common;
using ChessEngine.Exceptions;
using ChessEngine.Interfaces;

namespace ChessEngine.Pieces
{
    public abstract class Piece : IPiece
    {
        protected ISquare currentSquare;
        private readonly IPlayer player;
        private bool firstMove = true;

        public event EventHandler<TurnEventArgs> TurnHandler;
        [JsonIgnore]
        public IPlayer Player => player;
        [JsonIgnore]
        public ISquare Square => currentSquare;
        public string Type => this.GetType().Name;
        public (char file, char rank) Position => currentSquare.Position;
        public string Id => currentSquare.Id;
        public bool FirstMove => firstMove;

        public Piece(ISquare currentSquare, IPlayer player)
        {
            this.currentSquare = currentSquare;
            this.player = player;
            currentSquare.Piece = this;
        }

        public virtual void Move(ISquare newSquare)
        {
            bool pieceCaptured = false;
            IPiece capturedPiece = null;

            if (!player.Turn)
            {
                throw new InvalidMoveException("It is not the players turn");
            }

            if (newSquare.Occupied && newSquare.Piece.Player == this.player)
            {
                throw new InvalidMoveException("Square occupied by current player");
            }

            if(CheckRules(newSquare))
            {
                throw new InvalidMoveException();
            }

            if (newSquare.Occupied && newSquare.Piece.Player != this.player)
            {
                Capture(newSquare.Piece);
                pieceCaptured = true;
                capturedPiece = newSquare.Piece;
            }

            currentSquare.Piece = null;
            currentSquare = newSquare;
            newSquare.Piece = this;
            if (firstMove) firstMove = false;

            TurnHandler.Invoke(this, new TurnEventArgs(pieceCaptured, capturedPiece));
        }

        public void RemoveFromBoard()
        {
            currentSquare = null;
        }

        protected abstract bool CheckRules(ISquare newSquare);

        private void Capture(IPiece piece)
        {
            piece.Player.Pieces.Remove(piece);
            piece.RemoveFromBoard();
            this.Player.CapturedPieces.Add(piece);
        }
    }
}