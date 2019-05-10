using System;
using ChessEngine.Interfaces;

namespace ChessEngine.Common
{

    public class TurnEventArgs : EventArgs
    {
        public bool PieceCaptured { get; }
        public IPiece CapturedPiece { get; }
        public string TurnMessage { get; }

        public TurnEventArgs(bool pieceCaptured, IPiece capturedPiece, string turnMessage)
        {
            CapturedPiece = capturedPiece;
            TurnMessage = turnMessage;
            PieceCaptured = pieceCaptured;
        }
    }
}