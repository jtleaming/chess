using System;
using ChessEngine.Interfaces;

namespace ChessEngine.Common
{

    public class TurnEventArgs : EventArgs
    {
        public IPlayer Player { get; private set; }
        public TurnEventArgs(IPlayer player)
        {
            Player = player;
        }
    }
}