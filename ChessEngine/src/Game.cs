using System;
using System.Collections.Generic;
using System.Linq;
using ChessEngine.Interfaces;

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
            Players = (new Player(SetPlayerPieces(whiteStartIndex)){Turn = true}, new Player(SetPlayerPieces(blackStartIndex)){Turn = false});
        }

        private List<ISquare> SetPlayerPieces(int playerStartIndex)
        {
            return Board.Squares.GetRange(playerStartIndex, 16).ToList();
        }
    }
}