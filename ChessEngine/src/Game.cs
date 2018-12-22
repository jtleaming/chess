using System;
using System.Collections.Generic;
using System.Linq;
using ChessEngine.Interfaces;
using ChessEngine.Common;
using ChessEngine.Factory;
using ChessEngine.Extensions;

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
            PiecesFactory factory = new PiecesFactory();
            Board = new Board();
            Players = (new Player(SetPlayerPieces(whiteStartIndex), factory.GetPlayerPieces){ Turn = true }, 
                    new Player(SetPlayerPieces(blackStartIndex).Reverse<ISquare>(), factory.GetPlayerPieces) { Turn = false });

            Players.PlayerOne.Pieces.ForEach(p => p.TurnHandler += TurnListener);
            Players.PlayerTwo.Pieces.ForEach(p => p.TurnHandler += TurnListener);
        }

        private List<ISquare> SetPlayerPieces(int playerStartIndex)
        {
            return Board.Squares.ToList().GetRange(playerStartIndex, 16).Select(s => s.Value).ToList();
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