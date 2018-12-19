using System;
using System.Collections.Generic;
using System.Linq;
using ChessEngine.Interfaces;
using ChessEngine.Common;

namespace ChessEngine
{
    public class Game
    {
        public IBoard Board { get; set; }
        public (IPlayer PlayerOne, IPlayer PlayerTwo) Players { get; set; }
        private int whiteStartIndex = 0;
        private int blackStartIndex = 48;

        public void CreateGame()
        {
            Board = new Board();
            Players = (new Player(SetPlayerPieces(whiteStartIndex)) { Turn = true }, new Player(SetPlayerPieces(blackStartIndex)) { Turn = false });

            Players.PlayerOne.Pieces.ForEach(p => p.TurnHandler += TurnListener);
            Players.PlayerTwo.Pieces.ForEach(p => p.TurnHandler += TurnListener);
        }

        private Dictionary<string, ISquare> SetPlayerPieces(int playerStartIndex)
        {
            var pieces = Board.Squares.ToList().GetRange(playerStartIndex, 16);
            return pieces.ToDictionary(k => k.Key, v => v.Value);
        }

        private void TurnListener(object e, TurnEventArgs eventArgs)
        {
            if (Players.PlayerOne.Equals(eventArgs.Player))
            {
                Players.PlayerOne.Turn = false;
                Players.PlayerTwo.Turn = true;
            }
            else
            {
                Players.PlayerTwo.Turn = false;
                Players.PlayerOne.Turn = true;
            }
        }
    }
}