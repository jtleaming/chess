using System;
using ChessEngine.Interfaces;

namespace ChessEngine.Common
{

    public class TurnEventArgs : EventArgs
    {
        public bool PieceCaptured { get; }
        public IPiece CapturedPiece { get; }
        public TurnEventArgs(bool pieceCaptured, IPiece capturedPiece)
        {
            CapturedPiece = capturedPiece;
            PieceCaptured = pieceCaptured;
        }
    }
}