using System;
using System.Collections.Generic;
using ChessEngine.Interfaces;

namespace ChessEngine
{
    public class Game
    {
        public IBoard Board { get; set; }
        public IPlayers Players { get; set; }

        public void CreateGame()
        {
            Board = new Board();
            Players = new Players();
        }
    }
}